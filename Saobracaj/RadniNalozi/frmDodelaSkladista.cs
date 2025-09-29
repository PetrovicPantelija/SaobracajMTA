using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Diagnostics.CodeAnalysis;
using Saobracaj;
using System.Drawing;
using Saobracaj.Dokumenta;
using Syncfusion.Windows.Forms;
using System.Drawing.Imaging;
using Syncfusion.GridHelperClasses;
using Syncfusion.Windows.Forms.Grid.Grouping;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Grouping;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace Saobracaj.RadniNalozi
{
    public partial class frmDodelaSkladista : Form
    {
        int TipRadnogNaloga = 0;

        private void ChangeTextBox()
        {
            this.BackColor = Color.White;
            this.commandBarController1.Style = Syncfusion.Windows.Forms.VisualStyle.Office2010;
            this.commandBarController1.Office2010Theme = Office2010Theme.Managed;
            Office2010Colors.ApplyManagedColors(this, Color.White);
            //  toolStripHeader.BackColor = Color.FromArgb(240, 240, 248);
            //  toolStripHeader.ForeColor = Color.FromArgb(51, 51, 54);
     
            this.ControlBox = true;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            if (Saobracaj.Sifarnici.frmLogovanje.Firma == "Leget")
            {
               
                this.Icon = Saobracaj.Properties.Resources.LegetIconPNG;
                // this.FormBorderStyle = FormBorderStyle.None;
                tabSplitterContainer1.BackColor = Color.White;
                Office2010Colors.ApplyManagedColors(this, Color.White);




                foreach (Control control in tabSplitterPage1.Controls)
                {
                    if (control is Button buttons)
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
                ChangeTextBox();
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

            dgv.DefaultCellStyle.Font = new Font("Helvetica", 12F, GraphicsUnit.Pixel);
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

        public frmDodelaSkladista()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
        }

        public frmDodelaSkladista(string Prijem, int TipRN)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjgxNjY5QDMxMzkyZTM0MmUzMFVQcWRYSEJHSzU3b3kxb0xiYXhKbTR2WUQyZmhWTitWdFhjUEsvUXBPQ1E9");
            InitializeComponent();
            ChangeTextBox();
            TipRadnogNaloga = TipRN;
            textBox1.Text = Prijem;
            if (TipRN == 1)
            {
                //RN1PrijemOVoza
                label5.Text = "GATE IN VOZ";
                chkGateInVoz.Checked = true;
            };
            if (TipRN == 2)
            {
                //Prijem Platforme RN4 IZVOZ
                label5.Text = "GATE IN KAMION - RN4";
                chkGAteInKamion.Checked = true;

            }

            if (TipRN == 22)
            {
                //Prijem Platforme RN4 UVoz
                label5.Text = "GATE IN KAMION - RN4";
                chkGateInKamionUvoz.Checked = true;

            }

            if (TipRN == 21)
            {
                //Prijem Platforme RN4
                label5.Text = "GATE IN KAMION - RN5";
                chkGAteInKamion.Checked = true;

            }

            if (TipRN == 3)
            {
                label5.Text = "CIR";
                chkCIR.Checked = true;
            }
            if (TipRN == 4)
            {
                label5.Text = "GATE IN KAMION S1";
            };

            if (TipRN == 5)
            {
                label5.Text = "GATE IN KAMION TERMINAL";
                chkGateINTerminal.Checked = true;
            };
            if (TipRN == 6)
            {
                label5.Text = "Otprema ID";
            };

            if (TipRN == 12)
            {
                label5.Text = "INTERNI PRENOS";
            };


        }
        private void FillGVSkladista()
        {
            var select = "  Select ID, Naziv, Kapacitet ,  " +
" (Select Count(*) from KontejnerTekuce where KontejnerTekuce.Skladiste = Skladista.ID) as TrenutnoKontejnera " +
" From Skladista where GrupaPoljaID = 1";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kapacitet";
            dataGridView1.Columns[2].Width = 100;

        }
        private void frmDodelaSkladista_Load(object sender, EventArgs e)
        {

            FillGVSkladista();
           

        }

        private void FillDGRN1()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, NaSkladiste,Skladista.Naziv as Sklad, DatumRasporeda, NalogIzdao, Voz.BrVoza, Voz.NazivVoza,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
   " inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
   " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
   " inner join Skladista on Skladista.ID = NaSkladiste " +
   " inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
   " inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
   " inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemVoza.PrijemID = " + textBox1.Text + 
   " order by RNPrijemVoza.ID  ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];
            PodesiDatagridView(dataGridView2);

            dataGridView2.Columns["Sklad"].DefaultCellStyle.BackColor = Color.LightBlue;

        }

        private void FillDGRN1SF()
        {
            var select = "select RNPrijemVoza.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, NaSkladiste,Skladista.Naziv as Sklad, DatumRasporeda, NalogIzdao, Voz.BrVoza, Voz.NazivVoza,  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemVoza.Napomena, RNPrijemVoza.PrijemID,RNPrijemVoza.NalogID, DatumRealizacije, NalogRealizovao, Zavrsen  from RNPrijemVoza " +
   " inner join TipKontenjera on TipKontenjera.ID = RNPrijemVoza.VrstaKontejnera " +
   " inner join Voz on RNPrijemVoza.SaVoznogSredstva = Voz.ID " +
   " inner join Skladista on Skladista.ID = NaSkladiste " +
   " inner join Partnerji on Partnerji.PaSifra = RNPrijemVoza.Uvoznik " +
   " inner join Partnerji p2 on p2.PaSifra = RNPrijemVoza.NazivBrodara " +
   " inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemVoza.PrijemID = " + textBox1.Text +
   " order by RNPrijemVoza.ID  ";

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

            GridExcelFilter gridExcelFilter = new GridExcelFilter();

            //Wiring GridExcelFilter to GridGroupingControl
            gridExcelFilter.WireGrid(this.gridGroupingControl1);

            GridDynamicFilter dynamicFilter = new GridDynamicFilter();
            dynamicFilter.WireGrid(this.gridGroupingControl1);

        }

        private void FillDGRN21()
        {
            // Prijem platforme kalmarista
            var select = "select * from RnPrijemPlatforme 2" +
   " order by RnPrijemPlatforme2.ID  desc ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

        }

        private void FillDGRN2()
        {
            // Prijem platforme kalmarista
            var select = "  select RNPrijemPlatforme.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, RNPrijemPlatforme.Kamion, USkladiste,Skladista.Naziv as Sklad, " +
                " DatumRasporeda, NalogIzdao, " +
"  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, " +
" VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemPlatforme.Napomena, RNPrijemPlatforme.PrijemID,RNPrijemPlatforme.NalogID, DatumRealizacije, " +
" NalogRealizovao, RNPrijemPlatforme.Zavrsen from RNPrijemPlatforme " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemPlatforme.VrstaKontejnera " +
" inner join Skladista on Skladista.ID = USkladiste " +
" inner join Partnerji on Partnerji.PaSifra = RNPrijemPlatforme.Izvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemPlatforme.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemPlatforme.PrijemID = " + textBox1.Text +
   " order by RNPrijemPlatforme.ID  desc ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

        }

        private void FillDGRN4()
        {
            //Prijem platforme Uvoz - scenario 1
            var select = "  select RNPrijemPlatforme.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, RNPrijemPlatforme.Kamion, USkladiste,Skladista.Naziv as Polje, " +
                " DatumRasporeda, NalogIzdao, " +
"  PArtnerji.PaNaziv as Uvoznik, p2.PaNaziv as Brodar, " +
" VrstaManipulacije.Naziv as Usliga, BrojPlombe, RNPrijemPlatforme.Napomena, RNPrijemPlatforme.PrijemID,RNPrijemPlatforme.NalogID, DatumRealizacije, " +
" NalogRealizovao, RNPrijemPlatforme.Zavrsen from RNPrijemPlatforme " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemPlatforme.VrstaKontejnera " +
" inner join Skladista on Skladista.ID = USkladiste " +
" left join Partnerji on Partnerji.PaSifra = RNPrijemPlatforme.Izvoznik " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemPlatforme.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemPlatforme.PrijemID = " + textBox1.Text +
   " order by RNPrijemPlatforme.ID  desc ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

        }

        private void FillDGRN5()
        {
            //Prijem platforme TERMINAL BRODAR
            var select = "  select RNPrijemPlatforme2.ID,BrojKontejnera, TipKontenjera.Naziv as VrstaKontejnera, RNPrijemPlatforme2.Kamion, USkladiste,Skladista.Naziv as Polje, " +
" DatumRasporeda, NalogIzdao, " +
" '' as Uvoznik, p2.PaNaziv as Brodar, " +
" VrstaManipulacije.Naziv as Usliga, '' as BRojPlombe, RNPrijemPlatforme2.Napomena, RNPrijemPlatforme2.PrijemID,RNPrijemPlatforme2.NalogID, DatumRealizacije, " +
" NalogRealizovao, RNPrijemPlatforme2.Zavrsen from RNPrijemPlatforme2 " +
" inner join TipKontenjera on TipKontenjera.ID = RNPrijemPlatforme2.VrstaKontejnera " +
" inner join Skladista on Skladista.ID = USkladiste " +
" inner join Partnerji p2 on p2.PaSifra = RNPrijemPlatforme2.NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge where RNPrijemPlatforme2.PrijemID = " + textBox1.Text +
   " order by RNPrijemPlatforme2.ID  desc ";

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

        }

        private void FillDGRN12()
        {
            //Prijem platforme TERMINAL BRODAR
            var select = "SELECT       RNMedjuskladisni.ID as ID,  RNMedjuskladisni.BrojKontejnera,  RNMedjuskladisni.VrstaKontejnera,TipKontenjera.Naziv, " +
                  "Skladista.ID as SaID, Skladista.Naziv AS SkladSa, Skladista_1.ID AS NaID, Skladista_1.Naziv AS NaSkladiste," +
                  "                     RNMedjuskladisni.DatumRasporeda, " +
                 " RNMedjuskladisni.NalogIzdao, RNMedjuskladisni.DatumRealizacije, RNMedjuskladisni.NalogRealizovao, RNMedjuskladisni.Napomena, " +
                 " RNMedjuskladisni.Zavrsen FROM            RNMedjuskladisni INNER JOIN" +
                 " TipKontenjera ON RNMedjuskladisni.VrstaKontejnera = TipKontenjera.ID INNER JOIN" +
                 " Skladista ON RNMedjuskladisni.SaSkladista = Skladista.ID INNER JOIN   Skladista AS Skladista_1 ON RNMedjuskladisni.NaSkladiste = Skladista_1.ID where RNMedjuskladisni.ID = " + textBox1.Text;
       

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

        }

        private void FillDGRN6()
        {
            var select = "SELECT [RNOtpremaPlatforme].[ID] " +
    " ,[DatumRasporeda]      ,[BrojKontejnera] " +
   "    ,[VrstaKontejnera]      ,[NalogIzdao] " +
   "    ,[DatumRealizacije]      ,[Uvoznik] " +
   "    ,[CarinskiPostupak]      , VrstaCarinskogPostupka.Naziv as CarinskiPostupak " +
     "  ,[SpedicijaRTC]	  , p3.PaNaziv as SpedicijaRTC " +
    "   ,[NazivBrodara]      ,[VrstaRobe] " +
    "   ,[SaSkladista] as Polja	  , Skladista.Naziv as NazivPolja" +
     "  ,[SaPozicijeSklad]	  , Pozicija.Opis " +
     "  ,[IdUsluge]      ,[NalogRealizovao] " +
    "   ,[OtpremaID]      ,[Kamion] " +
    "   ,[Zavrsen]      ,[NalogID] " +
     "        FROM [dbo].[RNOtpremaPlatforme] " +
 "  inner join skladista on skladista.id = SaSkladista " +
 "  inner join Pozicija on Pozicija.ID = [SaPozicijeSklad] " +
" inner join Partnerji on Partnerji.PaSifra = [RNOtpremaPlatforme].Uvoznik " +
" inner join Partnerji p2 on p2.PaSifra = [RNOtpremaPlatforme].NazivBrodara " +
" inner join VrstaManipulacije on VrstaManipulacije.ID = IdUsluge " +
" inner join VrstaCarinskogPostupka on VrstaCarinskogPostupka.ID = [CarinskiPostupak] " +
" inner join Partnerji p3 on p3.PaSifra = [RNOtpremaPlatforme].[SpedicijaRTC]  where OtpremaID = " + textBox1.Text;

            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView2);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (TipRadnogNaloga == 1)
            {
                // FillDGRN1();
                dataGridView2.Visible = false;
                gridGroupingControl1.Visible = true;
                FillDGRN1SF();
            }    
            // RNPRIJEMVOZA

            if (TipRadnogNaloga == 2)
            {
                FillDGRN2();
                dataGridView2.Visible = true;
                gridGroupingControl1.Visible = false;
            }
            // RN PRIJEM PLATFORME IZVOZ

            if (TipRadnogNaloga == 22)
            { 
             FillDGRN4(); // RN PRIJEM PLATFORME IZVOZ
                dataGridView2.Visible = true;
                gridGroupingControl1.Visible = false;
            }


            if (TipRadnogNaloga == 21)
            {   FillDGRN21(); // 
                dataGridView2.Visible = true;
                gridGroupingControl1.Visible = false;

            }
            //   RN PRIJEM PLATFORME BRODAR

            if (TipRadnogNaloga == 6)
            { 
            FillDGRN6();
                dataGridView2.Visible = true;
                gridGroupingControl1.Visible = false;
            }


            if (TipRadnogNaloga == 5)
            { 
             FillDGRN5();
                dataGridView2.Visible = true;
                gridGroupingControl1.Visible = false;
            }

            if (TipRadnogNaloga == 12)
            { 
                 FillDGRN12();
                dataGridView2.Visible = true;
                gridGroupingControl1.Visible = false;
            }// MEDJUSKLADISNI
              
        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            if (TipRadnogNaloga == 1)
            {
                //Prijem voza
                foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN1Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        if (this.gridGroupingControl1.Table.SelectedRecords.Count > 0)
                        {
                            foreach (SelectedRecord selectedRecord in this.gridGroupingControl1.Table.SelectedRecords)
                            {
                               
                                ins.UpdateRN1Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(selectedRecord.Record.GetValue("ID").ToString()));
                                //To get the cell value of particular column of selected records   
                                //  string cellValue = selectedRecord.Record.GetValue("ID").ToString();
                                // MessageBox.Show(cellValue);
                            }
                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }


               

               // FillDGRN1();
               dataGridView2.Visible = false;

                FillDGRN1SF();

            }
            // Dodeljujemo privremeno skladiste na koje Kalmarista spusta
            if (TipRadnogNaloga == 2 )
            {
                //Prijem voza
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN4PrivremenoSkladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN2(); //

            }

            if (TipRadnogNaloga == 22)
            {
                //Prijem voza
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN4PrivremenoSkladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN4(); //

            }


            if (TipRadnogNaloga == 21)
            {
                //Prijem voza
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN4PrivremenoSkladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN21(); //

            }

            if (TipRadnogNaloga == 3)
            {
                //Prijem voza
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN4SkladisteIzCIRA(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN2(); //

            }


            if (TipRadnogNaloga == 4)
            {
                //Napisano za PrijemPlatvorme Uvoz
              
            foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN6Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN6();

            }


            if (TipRadnogNaloga == 5)
            {
                //Napisano za PrijemPlatvorme BRODAR - PRIJEM TERMINALA

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN5Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN6();

            }




            if (TipRadnogNaloga == 6)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN6Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN6();
            }


            if (TipRadnogNaloga == 12)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    InsertRN ins = new InsertRN();

                    if (row.Selected == true)
                        foreach (DataGridViewRow row2 in dataGridView2.Rows)
                        {
                            if (row2.Selected == true)
                            {
                                ins.UpdateRN12Skladiste(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row2.Cells[0].Value.ToString()));
                            }


                        }

                    // ins.UpdateOstaleStavke(Convert.ToInt32(row.Cells[0].Value.ToString()), Convert.ToInt32(row.Cells[1].Value.ToString()), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString(), Convert.ToDouble(row.Cells[7].Value.ToString()), Convert.ToDouble(row.Cells[8].Value.ToString()), Convert.ToDouble(row.Cells[9].Value.ToString()), Convert.ToDouble(row.Cells[10].Value.ToString()), Convert.ToDouble(row.Cells[11].Value.ToString()), Convert.ToDouble(row.Cells[12].Value.ToString()), Convert.ToDouble(row.Cells[13].Value.ToString()), Convert.ToDouble(row.Cells[14].Value.ToString()), row.Cells[15].Value.ToString(), row.Cells[18].Value.ToString(), row.Cells[19].Value.ToString(), Convert.ToDouble(row.Cells[20].Value.ToString()), row.Cells[23].Value.ToString(), row.Cells[24].Value.ToString());
                }
                FillDGRN12();
            }

        }

        private void FillGVSkladistaSuzeno()
        {
            var select = "  Select ID, Naziv, Kapacitet ,  " +
" (Select Count(*) from KontejnerTekuce where KontejnerTekuce.Skladiste = Skladista.ID) as TrenutnoKontejnera " +
" From Skladista where Skladista.Naziv like ('%"+ textBox2.Text +"%') order by Skladista.Naziv";
            var s_connection = Saobracaj.Sifarnici.frmLogovanje.connectionString;
            SqlConnection myConnection = new SqlConnection(s_connection);
            var c = new SqlConnection(s_connection);
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];

            PodesiDatagridView(dataGridView1);

            //string value = dataGridView3.Rows[0].Cells[0].Value.ToString();
            DataGridViewColumn column = dataGridView1.Columns[0];
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 30;

            DataGridViewColumn column1 = dataGridView1.Columns[1];
            dataGridView1.Columns[1].HeaderText = "Naziv";
            dataGridView1.Columns[1].Width = 80;

            DataGridViewColumn column2 = dataGridView1.Columns[2];
            dataGridView1.Columns[2].HeaderText = "Kapacitet";
            dataGridView1.Columns[2].Width = 100;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            FillGVSkladistaSuzeno();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            frmPregledSkladistaNovi pr = new frmPregledSkladistaNovi();
            pr.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            frmPregledSkladistaNovi pr = new frmPregledSkladistaNovi();
            pr.Show();
        }
    }
}
