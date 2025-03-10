using Microsoft.Office.Interop.Excel;
using Syncfusion.GridHelperClasses;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Grid.Grouping;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static Syncfusion.WinForms.Core.NativeScroll;

namespace Saobracaj.Dokumenta
{
    public partial class frmPopisKonterjnera : Form
    {
        bool status = false;
        public string connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
        string Korisnik = Sifarnici.frmLogovanje.user;

        private void ChangeTextBox()
        {
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
                Office2010Colors.ApplyManagedColors(this, Color.White);

                foreach (Control control in tabSplitterPage1.Controls)
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


                foreach (Control control in tabSplitterPage2.Controls)
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
                meniHeader.Visible = true;
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


        public frmPopisKonterjnera()
        {
            InitializeComponent();
            ChangeTextBox();
        }

        private void tsNew_Click(object sender, EventArgs e)
        {

        }

        private void tsSave_Click(object sender, EventArgs e)
        {

        }

        private void tsDelete_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmPopisKonterjnera_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);

            var select3 = " Select Distinct ID, Naziv   From Skladista";
            var s_connection3 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection3 = new SqlConnection(s_connection3);
            var c3 = new SqlConnection(s_connection3);
            var dataAdapter3 = new SqlDataAdapter(select3, c3);

            var commandBuilder3 = new SqlCommandBuilder(dataAdapter3);
            var ds3 = new DataSet();
            dataAdapter3.Fill(ds3);
         


            var select4 = " Select Distinct ID, Naziv   From Skladista";
            var s_connection4 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection4 = new SqlConnection(s_connection4);
            var c4 = new SqlConnection(s_connection4);
            var dataAdapter4 = new SqlDataAdapter(select4, c4);

            var commandBuilder4 = new SqlCommandBuilder(dataAdapter4);
            var ds4 = new DataSet();
            dataAdapter4.Fill(ds4);
            cboSkladisteNovo.DataSource = ds4.Tables[0];
            cboSkladisteNovo.DisplayMember = "Naziv";
            cboSkladisteNovo.ValueMember = "ID";


            var select41 = " Select Distinct ID, Naziv   From Skladista";
            var s_connection41 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection41 = new SqlConnection(s_connection41);
            var c41 = new SqlConnection(s_connection41);
            var dataAdapter41 = new SqlDataAdapter(select41, c41);
            var commandBuilder41 = new SqlCommandBuilder(dataAdapter41);
            var ds41 = new DataSet();
            dataAdapter41.Fill(ds41);
            cboSkladiste.DataSource = ds41.Tables[0];
            cboSkladiste.DisplayMember = "Naziv";
            cboSkladiste.ValueMember = "ID";


            var select51 = " Select Distinct ID, Oznaka   From Pozicija";
            var s_connection51 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection51 = new SqlConnection(s_connection51);
            var c51 = new SqlConnection(s_connection51);
            var dataAdapter51 = new SqlDataAdapter(select51, c51);
            var commandBuilder51 = new SqlCommandBuilder(dataAdapter51);
            var ds51 = new DataSet();
            dataAdapter51.Fill(ds51);
            cboPozicija.DataSource = ds51.Tables[0];
            cboPozicija.DisplayMember = "Oznaka";
            cboPozicija.ValueMember = "ID";


            var select52 = " Select Distinct ID, Oznaka   From Pozicija";
            var s_connection52 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection52 = new SqlConnection(s_connection52);
            var c52 = new SqlConnection(s_connection52);
            var dataAdapter52 = new SqlDataAdapter(select52, c52);
            var commandBuilder52 = new SqlCommandBuilder(dataAdapter52);
            var ds52 = new DataSet();
            dataAdapter52.Fill(ds52);
            cboPozicijaNova.DataSource = ds52.Tables[0];
            cboPozicijaNova.DisplayMember = "Oznaka";
            cboPozicijaNova.ValueMember = "ID";


            var partner5 = "Select PaSifra,PaNaziv From Partnerji order by PaNaziv";
            var partAD5 = new SqlDataAdapter(partner5, conn);
            var partDS5 = new DataSet();
            partAD5.Fill(partDS5);
            cboBrodar.DataSource = partDS5.Tables[0];
            cboBrodar.DisplayMember = "PaNaziv";
            cboBrodar.ValueMember = "PaSifra";

            var tipkontejnera = "Select ID, SkNaziv From TipKontenjera order by SkNaziv";
            var tkAD = new SqlDataAdapter(tipkontejnera, conn);
            var tkDS = new DataSet();
            tkAD.Fill(tkDS);
            cboTipKontejnera.DataSource = tkDS.Tables[0];
            cboTipKontejnera.DisplayMember = "SkNaziv";
            cboTipKontejnera.ValueMember = "ID";


            var statuskontejnera = "Select ID, Naziv From KontejnerStatus order by Naziv";
            var skAD = new SqlDataAdapter(statuskontejnera, conn);
            var skDS = new DataSet();
            skAD.Fill(skDS);
            cboStatusKontejnera.DataSource = skDS.Tables[0];
            cboStatusKontejnera.DisplayMember = "Naziv";
            cboStatusKontejnera.ValueMember = "ID";


            var select6 = " Select Distinct ID, Naziv   From uvKvalitetKontejnera order by ID";
            var s_connection6 = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection6 = new SqlConnection(s_connection6);
            var c6 = new SqlConnection(s_connection6);
            var dataAdapter6 = new SqlDataAdapter(select6, c6);

            var commandBuilder6 = new SqlCommandBuilder(dataAdapter6);
            var ds6 = new DataSet();
            dataAdapter6.Fill(ds6);
            cboKvalitet.DataSource = ds6.Tables[0];
            cboKvalitet.DisplayMember = "Naziv";
            cboKvalitet.ValueMember = "ID";

            RefreshDataGridPopisi();

        }

        private void tsNew_Click_1(object sender, EventArgs e)
        {
            status = true;
            txtSifra.Text = "";
            dtpDatumPopisa.Value = DateTime.Now;
            txtNapomena.Text = "";
        }

        private void tsSave_Click_1(object sender, EventArgs e)
        {
            if (status == true)
            {
               InsertPopisKontejnera ins = new InsertPopisKontejnera();
                ins.InsPopisKontejnera(Convert.ToDateTime(dtpDatumPopisa.Value), txtNapomena.Text, DateTime.Now,    Korisnik);
            }
            else
            {
                InsertPopisKontejnera upd = new InsertPopisKontejnera();
                upd.UpdPopisKontejnera(Convert.ToInt32(txtSifra.Text), Convert.ToDateTime(dtpDatumPopisa.Value), txtNapomena.Text, DateTime.Now, Korisnik);
            }
            RefreshDataGridPopisi() ;
            RefreshDataGrid();
            RefreshDataGridGT();

        }

        private void RefreshDataGridPopisi()
        {
            var select = "  SELECT [ID]  ,[DatumPopisa],[Napomena],[Datum],[Korisnik] FROM [dbo].[PopisKontejnera] order by ID desc";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = false;
            dataGridView2.DataSource = ds.Tables[0];
            //   dataGridView2.Columns["StvarnoStanje"].ReadOnly = false;
            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView2);
            }
            else
            {
                dataGridView2.BorderStyle = BorderStyle.None;
                dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView2.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView2.BackgroundColor = Color.White;

                dataGridView2.EnableHeadersVisualStyles = false;
                dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }

            DataGridViewColumn column = dataGridView2.Columns[0];
            dataGridView2.Columns[0].HeaderText = "ID";
            dataGridView2.Columns[0].Width = 50;
      

        }

        private void RefreshDataGrid()
        {
            if (txtSifra.Text == "")
                return;
            var select = "         SELECT [PopisKontejneraStavke].[ID]      ,[PopisKontejneraStavke].[IDNadredjenog]  as NAdredjeni" +
     " ,[PopisKontejneraStavke].[BrojKontejnera]      ,Skladista.Naziv as TekuceSkladiste " +
     "  ,Pozicija.Oznaka as TekucaPozicija      ,[PronadjenIspravan] " +
    "   ,s2.Naziv as SkladisteNovo      ,p2.Oznaka " +
    "   ,[PopisKontejneraStavke].[Datum]      ,[PopisKontejneraStavke].[Korisnik] " +
     "  ,Partnerji.PaNAziv as Brodar     ,TipKontenjera.SkNaziv as VrstaKontejnera " +
     "  ,KontejnerStatus.Naziv as StatusKonterjnera, Generisan  FROM [dbo].[PopisKontejneraStavke] " +
     "    left  join TipKontenjera on TipKontenjera.ID = PopisKontejneraStavke.TipKontejnera " +
     "    left  join KontejnerStatus on KontejnerStatus.ID = PopisKontejneraStavke.[StatusKontejnera] " +
      "   left  join Partnerji on Partnerji.PaSifra = PopisKontejneraStavke.[Brodar] " +
      "   inner join Skladista on Skladista.ID = PopisKontejneraStavke.[SkladisteU] " +
      "   inner join Skladista as s2 on s2.ID = PopisKontejneraStavke.[SkladisteNovo] " +
      "   inner join Pozicija on Pozicija.ID = PopisKontejneraStavke.[LokacijaU] " +
      "   inner join Pozicija as p2 on p2.ID = PopisKontejneraStavke.[LokacijaNovo] " +
      "   where IDNadredjenog = " + txtSifra.Text + " Order by [PopisKontejneraStavke].[ID]  desc ";



            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);


            dataGridView3.ReadOnly = true;
            dataGridView3.DataSource = ds.Tables[0];

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
                PodesiDatagridView(dataGridView2);
            }
            else
            {
                dataGridView3.BorderStyle = BorderStyle.None;
                dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView3.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView3.BackgroundColor = Color.White;

                dataGridView3.EnableHeadersVisualStyles = false;
                dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }

            DataGridViewColumn column = dataGridView3.Columns[0];
            dataGridView3.Columns[0].HeaderText = "Šifra";
            dataGridView3.Columns[0].Width = 40;

            DataGridViewColumn column2 = dataGridView3.Columns[1];
            dataGridView3.Columns[1].HeaderText = "Naziv";
            dataGridView3.Columns[1].Width = 150;


            RefreshDataGridColors();


        }


        private void RefreshDataGridGT()
        {
            if (txtSifra.Text == "")
                return;
            var select = "         SELECT [PopisKontejneraStavke].[ID]      ,[PopisKontejneraStavke].[IDNadredjenog]  as NAdredjeni" +
     "    ,[PopisKontejneraStavke].[BrojKontejnera]      ,Skladista.Naziv as TekuceSkladiste " +
     "    ,PronadjenIspravan " +
    "     ,s2.Naziv as SkladisteNovo  " +    
     "     ,Partnerji.PaNAziv as Brodar     ,TipKontenjera.SkNaziv as VrstaKontejnera, kk.Naziv as KvalitetKontejnera " +
     "    ,KontejnerStatus.Naziv as StatusKonterjnera, Generisan,  Pozicija.Oznaka as TekucaPozicija  " +
     "    ,p2.Oznaka  ,[PopisKontejneraStavke].[Datum]      ,[PopisKontejneraStavke].[Korisnik]  FROM [dbo].[PopisKontejneraStavke] " +
     "    left  join TipKontenjera on TipKontenjera.ID = PopisKontejneraStavke.TipKontejnera " +
     "    left  join KontejnerStatus on KontejnerStatus.ID = PopisKontejneraStavke.[StatusKontejnera] " +
      "   left  join Partnerji on Partnerji.PaSifra = PopisKontejneraStavke.[Brodar] " +
      "   inner join Skladista on Skladista.ID = PopisKontejneraStavke.[SkladisteU] " +
      "   inner join Skladista as s2 on s2.ID = PopisKontejneraStavke.[SkladisteNovo] " +
      "   inner join Pozicija on Pozicija.ID = PopisKontejneraStavke.[LokacijaU] " +
      "   inner join Pozicija as p2 on p2.ID = PopisKontejneraStavke.[LokacijaNovo] " +
        "   inner join uvKvalitetKontejnera as kk on kk.ID = PopisKontejneraStavke.[Kvalitet] " +
      "   where IDNadredjenog = " + txtSifra.Text + " Order by [PopisKontejneraStavke].[ID]  desc ";



            var s_connection = Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            // dataGridView1.ReadOnly = true;
            this.gridGroupingControl1.DataSource = ds.Tables[0];
            this.gridGroupingControl1.ShowGroupDropArea = true;
            this.gridGroupingControl1.TopLevelGroupOptions.ShowFilterBar = true;
        
          


            
            
           

            foreach (GridColumnDescriptor column in this.gridGroupingControl1.TableDescriptor.Columns)
            {
                column.AllowFilter = true;
            }

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);

            GridConditionalFormatDescriptor gcfd3 = new GridConditionalFormatDescriptor();
            gcfd3.Appearance.AnyRecordFieldCell.BackColor = Color.Green;
            gcfd3.Appearance.AnyRecordFieldCell.TextColor = Color.Yellow;

            gcfd3.Expression = "[PronadjenIspravan] LIKE \'1'";
            gcfd3.Name = "ConditionalFormat 1";



            GridConditionalFormatDescriptor gcfd31 = new GridConditionalFormatDescriptor();
            gcfd31.Appearance.AnyRecordFieldCell.BackColor = Color.Red;
            gcfd31.Appearance.AnyRecordFieldCell.TextColor = Color.White;

            gcfd31.Expression = "[Generisan] LIKE \'0'";
            gcfd31.Name = "ConditionalFormat 2";



            GridConditionalFormatDescriptor gcfd = new GridConditionalFormatDescriptor();
            gcfd.Appearance.AnyRecordFieldCell.BackColor = Color.Orange;
            gcfd.Appearance.AnyRecordFieldCell.TextColor = Color.Black;

            gcfd.Expression = "[SkladisteNovo] != \\";
            gcfd.Name = "ConditionalFormat 0";
            //To add the conditional format instances to the ConditionalFormats collection. 
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd);
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd3);
            this.gridGroupingControl1.TableDescriptor.ConditionalFormats.Add(gcfd31);
        }

        private void RefreshDataGridColors()
        {
            // Ako je u redu zeleno
            foreach (DataGridViewRow row in dataGridView3.Rows)
            { 
                if (Convert.ToInt32(row.Cells[5].Value) == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (row.Cells[6].Value.ToString().Trim() != "\\")
                { 
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    row.DefaultCellStyle.ForeColor = Color.White;   
                }
                else if (Convert.ToInt32(row.Cells[13].Value) == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.DarkRed;
                    row.DefaultCellStyle.ForeColor = Color.White;

                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pronadjen = 0;
            if (chkPronadjen.Checked == true)
                pronadjen = 1;
            InsertPopisKontejnera ins = new InsertPopisKontejnera();
            ins.InsPopisKontejneraStavke(Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, Convert.ToInt32(cboSkladiste.SelectedValue), Convert.ToInt32(cboPozicija.SelectedValue)
                , pronadjen, Convert.ToInt32(cboSkladisteNovo.SelectedValue), Convert.ToInt32(cboPozicijaNova.SelectedValue), DateTime.Now, Korisnik
                , Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue));
            RefreshDataGrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int pronadjen = 0;
            if (chkPronadjen.Checked == true)
                pronadjen = 1;
            InsertPopisKontejnera ins = new InsertPopisKontejnera();
            ins.UpdPopisKontejneraStavke(Convert.ToInt32(txtID.Text), Convert.ToInt32(txtSifra.Text), txtBrojKontejnera.Text, Convert.ToInt32(cboSkladiste.SelectedValue), Convert.ToInt32(cboPozicija.SelectedValue)
                , pronadjen, Convert.ToInt32(cboSkladisteNovo.SelectedValue), Convert.ToInt32(cboPozicijaNova.SelectedValue), DateTime.Now, Korisnik
                , Convert.ToInt32(cboBrodar.SelectedValue), Convert.ToInt32(cboTipKontejnera.SelectedValue), Convert.ToInt32(cboStatusKontejnera.SelectedValue), Convert.ToInt32(cboKvalitet.SelectedValue));
            RefreshDataGrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            InsertPopisKontejnera ins = new InsertPopisKontejnera();
            ins.DelPopisKontejneraStavke(Convert.ToInt32(txtID.Text));
            RefreshDataGrid();
        }

        private void VratiPodatkeStavke(string IdNadredjenog, int Stavka)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT [ID] " +
                "  , [IDNadredjenog]      , [BrojKontejnera]      , [SkladisteU]      , [LokacijaU] " +
                "  , [PronadjenIspravan]      , [SkladisteNovo]      , [LokacijaNovo]      , [Datum] " +
                "  , [Korisnik]      , [Brodar]      , [TipKontejnera]      , [StatusKontejnera], Kvalitet " +
              " FROM [dbo].[PopisKontejneraStavke] " +
                         " where IdNadredjenog = " + txtSifra.Text + " and ID = " + Stavka, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtID.Text = dr["ID"].ToString();
                txtBrojKontejnera.Text = dr["BrojKontejnera"].ToString();
                cboSkladiste.SelectedValue = Convert.ToInt32(dr["SkladisteU"].ToString());
                cboPozicija.SelectedValue = Convert.ToInt32(dr["LokacijaU"].ToString());
                cboSkladisteNovo.SelectedValue = Convert.ToInt32(dr["SkladisteNovo"].ToString());
                cboPozicijaNova.SelectedValue = Convert.ToInt32(dr["LokacijaNovo"].ToString());
                txtkorisanikPopisao.Text = dr["Korisnik"].ToString();
                cboBrodar.SelectedValue = Convert.ToInt32(dr["Brodar"].ToString());
                cboTipKontejnera.SelectedValue = Convert.ToInt32(dr["TipKontejnera"].ToString());
                cboStatusKontejnera.SelectedValue = Convert.ToInt32(dr["StatusKontejnera"].ToString());
                cboKvalitet.SelectedValue = Convert.ToInt32(dr["Kvalitet"].ToString());
                if (dr["PronadjenIspravan"].ToString() == "1")
                        chkPronadjen.Checked = true;

                dtpPopisStavke.Value = Convert.ToDateTime(dr["Datum"].ToString());
            }

            con.Close();
        }

        private void VratiPodatke(string Id)
        {
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection con = new SqlConnection(s_connection);

            con.Open();

            SqlCommand cmd = new SqlCommand(" SELECT ID, [DatumPopisa]     ,[Napomena] ,[Datum] ,[Korisnik]" +
  " FROM [dbo].[PopisKontejnera] " +
             " where  ID = " + Id, con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSifra.Text = dr["ID"].ToString();
                dtpDatumPopisa.Value = Convert.ToDateTime(dr["DatumPopisa"].ToString());
                txtNapomena.Text = dr["Napomena"].ToString();
            }

            con.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[1].Value.ToString();
                        txtID.Text = row.Cells[0].Value.ToString();
                        VratiPodatkeStavke(txtSifra.Text, Convert.ToInt32(row.Cells[0].Value.ToString()));
                    }
                }
            }
            catch
            {

            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Selected)
                    {
                        txtSifra.Text = row.Cells[0].Value.ToString();
                        VratiPodatke(txtSifra.Text);
                        RefreshDataGrid();
                        RefreshDataGridGT();
                    }
                }
            }
            catch
            {

            }
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            InsertPopisKontejnera ins = new InsertPopisKontejnera();
            ins.IUpisiSveStavkeKontejnerTekuce(Convert.ToInt32(txtSifra.Text));
            RefreshDataGrid();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            RefreshDataGrid();
            RefreshDataGridGT();
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {
            frmPopisKontejneraStampa pks = new frmPopisKontejneraStampa(txtID.Text);
            pks.Show();
        }

        private void gridGroupingControl1_TableControlCellClick(object sender, GridTableControlCellClickEventArgs e)
        {
            try
            {
                if (this.gridGroupingControl1.Table.CurrentRecord != null)
                {
                    txtID.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                     VratiPodatkeStavke(txtSifra.Text,Convert.ToInt32(txtID.Text) );

                   

                    //  RefreshDataGridGT();

                    // txtSifra.Text = gridGroupingControl1.Table.CurrentRecord.GetValue("ID").ToString();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void tabSplitterPage2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
