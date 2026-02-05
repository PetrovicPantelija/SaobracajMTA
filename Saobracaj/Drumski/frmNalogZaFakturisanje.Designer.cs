namespace Saobracaj.Drumski
{
    partial class frmNalogZaFakturisanje
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnObjedinjenaDokumentaNalogID = new System.Windows.Forms.Button();
            this.btnObjedinjenaDokumentaIFaktura = new System.Windows.Forms.Button();
            this.btnBezIzlazneFakture = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.lblBooking = new System.Windows.Forms.Label();
            this.lblBL = new System.Windows.Forms.Label();
            this.lbBrojCNT = new System.Windows.Forms.Label();
            this.lblContainerID = new System.Windows.Forms.Label();
            this.txtBooking = new System.Windows.Forms.TextBox();
            this.txtBL = new System.Windows.Forms.TextBox();
            this.txtBrojKontejnera = new System.Windows.Forms.TextBox();
            this.txtKontejnerID = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtBrojlzlazneFakture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNalog = new System.Windows.Forms.TextBox();
            this.btnIzadjiBezPromena = new System.Windows.Forms.Button();
            this.btnPotvrdi = new System.Windows.Forms.Button();
            this.dtpPregleda = new System.Windows.Forms.DateTimePicker();
            this.dtpDatumIzlazneFakture = new System.Windows.Forms.DateTimePicker();
            this.txtNalogodavac = new System.Windows.Forms.TextBox();
            this.lblDatumPregleda = new System.Windows.Forms.Label();
            this.lblRelacija = new System.Windows.Forms.Label();
            this.txtArtikal = new System.Windows.Forms.TextBox();
            this.lblNalogodavac = new System.Windows.Forms.Label();
            this.lblPrevoznik = new System.Windows.Forms.Label();
            this.txtRelacija = new System.Windows.Forms.TextBox();
            this.lblArtikal = new System.Windows.Forms.Label();
            this.txtPrilozenaDokumenta = new System.Windows.Forms.TextBox();
            this.txtKamioner = new System.Windows.Forms.TextBox();
            this.lblCena = new System.Windows.Forms.Label();
            this.txtBrojUlazneFakture = new System.Windows.Forms.TextBox();
            this.lblPrilozenaDokumenta = new System.Windows.Forms.Label();
            this.lblDatumIzlazneFakture = new System.Windows.Forms.Label();
            this.txtCenaTransporta = new System.Windows.Forms.TextBox();
            this.lblBrojUlazneFakture = new System.Windows.Forms.Label();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.detaljiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnObjedinjenaDokumentaNalogID);
            this.panel1.Controls.Add(this.btnObjedinjenaDokumentaIFaktura);
            this.panel1.Controls.Add(this.btnBezIzlazneFakture);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblBooking);
            this.panel1.Controls.Add(this.lblBL);
            this.panel1.Controls.Add(this.lbBrojCNT);
            this.panel1.Controls.Add(this.lblContainerID);
            this.panel1.Controls.Add(this.txtBooking);
            this.panel1.Controls.Add(this.txtBL);
            this.panel1.Controls.Add(this.txtBrojKontejnera);
            this.panel1.Controls.Add(this.txtKontejnerID);
            this.panel1.Location = new System.Drawing.Point(4, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1213, 100);
            this.panel1.TabIndex = 0;
            // 
            // btnObjedinjenaDokumentaNalogID
            // 
            this.btnObjedinjenaDokumentaNalogID.Location = new System.Drawing.Point(927, 29);
            this.btnObjedinjenaDokumentaNalogID.Name = "btnObjedinjenaDokumentaNalogID";
            this.btnObjedinjenaDokumentaNalogID.Size = new System.Drawing.Size(286, 27);
            this.btnObjedinjenaDokumentaNalogID.TabIndex = 24;
            this.btnObjedinjenaDokumentaNalogID.Text = "Objedinjena dokument po broju naloga";
            this.btnObjedinjenaDokumentaNalogID.UseVisualStyleBackColor = true;
            this.btnObjedinjenaDokumentaNalogID.Click += new System.EventHandler(this.btnObjedinjenaDokumentaNalogID_Click);
            // 
            // btnObjedinjenaDokumentaIFaktura
            // 
            this.btnObjedinjenaDokumentaIFaktura.Location = new System.Drawing.Point(927, 69);
            this.btnObjedinjenaDokumentaIFaktura.Name = "btnObjedinjenaDokumentaIFaktura";
            this.btnObjedinjenaDokumentaIFaktura.Size = new System.Drawing.Size(286, 27);
            this.btnObjedinjenaDokumentaIFaktura.TabIndex = 23;
            this.btnObjedinjenaDokumentaIFaktura.Text = "Objedinjena dokumenta po izlaznoj fakturi";
            this.btnObjedinjenaDokumentaIFaktura.UseVisualStyleBackColor = true;
            this.btnObjedinjenaDokumentaIFaktura.Click += new System.EventHandler(this.btnObjedinjenaDokumentaIFaktura_Click);
            // 
            // btnBezIzlazneFakture
            // 
            this.btnBezIzlazneFakture.Location = new System.Drawing.Point(187, 64);
            this.btnBezIzlazneFakture.Name = "btnBezIzlazneFakture";
            this.btnBezIzlazneFakture.Size = new System.Drawing.Size(198, 27);
            this.btnBezIzlazneFakture.TabIndex = 22;
            this.btnBezIzlazneFakture.Text = "Nalozi bez izlazne fakture";
            this.btnBezIzlazneFakture.UseVisualStyleBackColor = true;
            this.btnBezIzlazneFakture.Click += new System.EventHandler(this.btnBezIzlazneFakture_Click_1);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(8, 66);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(163, 22);
            this.dateTimePicker1.TabIndex = 21;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(763, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 27);
            this.button1.TabIndex = 20;
            this.button1.Text = "Pretraži";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblBooking
            // 
            this.lblBooking.AutoSize = true;
            this.lblBooking.Location = new System.Drawing.Point(549, 10);
            this.lblBooking.Name = "lblBooking";
            this.lblBooking.Size = new System.Drawing.Size(57, 16);
            this.lblBooking.TabIndex = 7;
            this.lblBooking.Text = "Booking";
            // 
            // lblBL
            // 
            this.lblBL.AutoSize = true;
            this.lblBL.Location = new System.Drawing.Point(395, 10);
            this.lblBL.Name = "lblBL";
            this.lblBL.Size = new System.Drawing.Size(23, 16);
            this.lblBL.TabIndex = 6;
            this.lblBL.Text = "BL";
            // 
            // lbBrojCNT
            // 
            this.lbBrojCNT.AutoSize = true;
            this.lbBrojCNT.Location = new System.Drawing.Point(184, 10);
            this.lbBrojCNT.Name = "lbBrojCNT";
            this.lbBrojCNT.Size = new System.Drawing.Size(62, 16);
            this.lbBrojCNT.TabIndex = 5;
            this.lbBrojCNT.Text = "Broj CNT";
            // 
            // lblContainerID
            // 
            this.lblContainerID.AutoSize = true;
            this.lblContainerID.Location = new System.Drawing.Point(10, 10);
            this.lblContainerID.Name = "lblContainerID";
            this.lblContainerID.Size = new System.Drawing.Size(79, 16);
            this.lblContainerID.TabIndex = 4;
            this.lblContainerID.Text = "Kontejner ID";
            // 
            // txtBooking
            // 
            this.txtBooking.Location = new System.Drawing.Point(552, 29);
            this.txtBooking.Name = "txtBooking";
            this.txtBooking.Size = new System.Drawing.Size(196, 22);
            this.txtBooking.TabIndex = 15;
            // 
            // txtBL
            // 
            this.txtBL.Location = new System.Drawing.Point(391, 29);
            this.txtBL.Name = "txtBL";
            this.txtBL.Size = new System.Drawing.Size(155, 22);
            this.txtBL.TabIndex = 10;
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Location = new System.Drawing.Point(187, 29);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.Size = new System.Drawing.Size(198, 22);
            this.txtBrojKontejnera.TabIndex = 5;
            // 
            // txtKontejnerID
            // 
            this.txtKontejnerID.Location = new System.Drawing.Point(8, 29);
            this.txtKontejnerID.Name = "txtKontejnerID";
            this.txtKontejnerID.Size = new System.Drawing.Size(168, 22);
            this.txtKontejnerID.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.gridGroupingControl1);
            this.panel2.Location = new System.Drawing.Point(4, 116);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1213, 473);
            this.panel2.TabIndex = 1;
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridGroupingControl1.ApplyVisualStyles = false;
            this.gridGroupingControl1.BackColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Custom;
            this.gridGroupingControl1.Location = new System.Drawing.Point(0, 0);
            this.gridGroupingControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2007ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2007ColorScheme.Black;
            this.gridGroupingControl1.Office2010ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2010ColorScheme.Black;
            this.gridGroupingControl1.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.Black;
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(1213, 473);
            this.gridGroupingControl1.TabIndex = 477;
            this.gridGroupingControl1.TableDescriptor.AllowEdit = false;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 22;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl1.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.One;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(199)))), ((int)(((byte)(249)))));
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "18.4460.0.34";
            this.gridGroupingControl1.TableControlMouseDown += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlMouseEventHandler(this.GridGroupingControl1_TableControlMouseDown);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.txtBrojlzlazneFakture);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtNalog);
            this.panel3.Controls.Add(this.btnIzadjiBezPromena);
            this.panel3.Controls.Add(this.btnPotvrdi);
            this.panel3.Controls.Add(this.dtpPregleda);
            this.panel3.Controls.Add(this.dtpDatumIzlazneFakture);
            this.panel3.Controls.Add(this.txtNalogodavac);
            this.panel3.Controls.Add(this.lblDatumPregleda);
            this.panel3.Controls.Add(this.lblRelacija);
            this.panel3.Controls.Add(this.txtArtikal);
            this.panel3.Controls.Add(this.lblNalogodavac);
            this.panel3.Controls.Add(this.lblPrevoznik);
            this.panel3.Controls.Add(this.txtRelacija);
            this.panel3.Controls.Add(this.lblArtikal);
            this.panel3.Controls.Add(this.txtPrilozenaDokumenta);
            this.panel3.Controls.Add(this.txtKamioner);
            this.panel3.Controls.Add(this.lblCena);
            this.panel3.Controls.Add(this.txtBrojUlazneFakture);
            this.panel3.Controls.Add(this.lblPrilozenaDokumenta);
            this.panel3.Controls.Add(this.lblDatumIzlazneFakture);
            this.panel3.Controls.Add(this.txtCenaTransporta);
            this.panel3.Controls.Add(this.lblBrojUlazneFakture);
            this.panel3.Location = new System.Drawing.Point(4, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1213, 450);
            this.panel3.TabIndex = 2;
            // 
            // txtBrojlzlazneFakture
            // 
            this.txtBrojlzlazneFakture.Location = new System.Drawing.Point(195, 333);
            this.txtBrojlzlazneFakture.Margin = new System.Windows.Forms.Padding(4);
            this.txtBrojlzlazneFakture.Name = "txtBrojlzlazneFakture";
            this.txtBrojlzlazneFakture.Size = new System.Drawing.Size(332, 22);
            this.txtBrojlzlazneFakture.TabIndex = 486;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 333);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 16);
            this.label2.TabIndex = 487;
            this.label2.Text = "Broj izlazne fakture";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 85);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 485;
            this.label1.Text = "Nalog ID";
            // 
            // txtNalog
            // 
            this.txtNalog.Location = new System.Drawing.Point(193, 85);
            this.txtNalog.Margin = new System.Windows.Forms.Padding(4);
            this.txtNalog.Name = "txtNalog";
            this.txtNalog.ReadOnly = true;
            this.txtNalog.Size = new System.Drawing.Size(332, 22);
            this.txtNalog.TabIndex = 484;
            // 
            // btnIzadjiBezPromena
            // 
            this.btnIzadjiBezPromena.Location = new System.Drawing.Point(372, 391);
            this.btnIzadjiBezPromena.Name = "btnIzadjiBezPromena";
            this.btnIzadjiBezPromena.Size = new System.Drawing.Size(154, 27);
            this.btnIzadjiBezPromena.TabIndex = 483;
            this.btnIzadjiBezPromena.Text = "Izađi bez promena";
            this.btnIzadjiBezPromena.UseVisualStyleBackColor = true;
            this.btnIzadjiBezPromena.Click += new System.EventHandler(this.btnIzadjiBezPromena_Click_1);
            // 
            // btnPotvrdi
            // 
            this.btnPotvrdi.Location = new System.Drawing.Point(194, 391);
            this.btnPotvrdi.Name = "btnPotvrdi";
            this.btnPotvrdi.Size = new System.Drawing.Size(154, 27);
            this.btnPotvrdi.TabIndex = 70;
            this.btnPotvrdi.Text = "Potvrdi izlaznu fakturu";
            this.btnPotvrdi.UseVisualStyleBackColor = true;
            this.btnPotvrdi.Click += new System.EventHandler(this.btnPotvrdi_Click);
            // 
            // dtpPregleda
            // 
            this.dtpPregleda.CustomFormat = "dd.MM.yyyy";
            this.dtpPregleda.Enabled = false;
            this.dtpPregleda.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPregleda.Location = new System.Drawing.Point(194, 18);
            this.dtpPregleda.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpPregleda.Name = "dtpPregleda";
            this.dtpPregleda.Size = new System.Drawing.Size(192, 22);
            this.dtpPregleda.TabIndex = 25;
            this.dtpPregleda.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // dtpDatumIzlazneFakture
            // 
            this.dtpDatumIzlazneFakture.CustomFormat = "dd.MM.yyyy";
            this.dtpDatumIzlazneFakture.Enabled = false;
            this.dtpDatumIzlazneFakture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDatumIzlazneFakture.Location = new System.Drawing.Point(194, 49);
            this.dtpDatumIzlazneFakture.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpDatumIzlazneFakture.Name = "dtpDatumIzlazneFakture";
            this.dtpDatumIzlazneFakture.Size = new System.Drawing.Size(192, 22);
            this.dtpDatumIzlazneFakture.TabIndex = 30;
            this.dtpDatumIzlazneFakture.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // txtNalogodavac
            // 
            this.txtNalogodavac.Location = new System.Drawing.Point(194, 174);
            this.txtNalogodavac.Margin = new System.Windows.Forms.Padding(4);
            this.txtNalogodavac.Name = "txtNalogodavac";
            this.txtNalogodavac.ReadOnly = true;
            this.txtNalogodavac.Size = new System.Drawing.Size(332, 22);
            this.txtNalogodavac.TabIndex = 45;
            // 
            // lblDatumPregleda
            // 
            this.lblDatumPregleda.AutoSize = true;
            this.lblDatumPregleda.Location = new System.Drawing.Point(19, 26);
            this.lblDatumPregleda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatumPregleda.Name = "lblDatumPregleda";
            this.lblDatumPregleda.Size = new System.Drawing.Size(104, 16);
            this.lblDatumPregleda.TabIndex = 482;
            this.lblDatumPregleda.Text = "Datum pregleda";
            // 
            // lblRelacija
            // 
            this.lblRelacija.AutoSize = true;
            this.lblRelacija.Location = new System.Drawing.Point(20, 241);
            this.lblRelacija.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelacija.Name = "lblRelacija";
            this.lblRelacija.Size = new System.Drawing.Size(57, 16);
            this.lblRelacija.TabIndex = 474;
            this.lblRelacija.Text = "Relacija";
            // 
            // txtArtikal
            // 
            this.txtArtikal.Location = new System.Drawing.Point(194, 206);
            this.txtArtikal.Margin = new System.Windows.Forms.Padding(4);
            this.txtArtikal.Name = "txtArtikal";
            this.txtArtikal.ReadOnly = true;
            this.txtArtikal.Size = new System.Drawing.Size(332, 22);
            this.txtArtikal.TabIndex = 50;
            // 
            // lblNalogodavac
            // 
            this.lblNalogodavac.AutoSize = true;
            this.lblNalogodavac.Location = new System.Drawing.Point(19, 178);
            this.lblNalogodavac.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNalogodavac.Name = "lblNalogodavac";
            this.lblNalogodavac.Size = new System.Drawing.Size(90, 16);
            this.lblNalogodavac.TabIndex = 472;
            this.lblNalogodavac.Text = "Nalogodavac";
            // 
            // lblPrevoznik
            // 
            this.lblPrevoznik.AutoSize = true;
            this.lblPrevoznik.Location = new System.Drawing.Point(20, 114);
            this.lblPrevoznik.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrevoznik.Name = "lblPrevoznik";
            this.lblPrevoznik.Size = new System.Drawing.Size(133, 16);
            this.lblPrevoznik.TabIndex = 466;
            this.lblPrevoznik.Text = "Prevoznik / Kamioner";
            // 
            // txtRelacija
            // 
            this.txtRelacija.Location = new System.Drawing.Point(194, 238);
            this.txtRelacija.Margin = new System.Windows.Forms.Padding(4);
            this.txtRelacija.Name = "txtRelacija";
            this.txtRelacija.ReadOnly = true;
            this.txtRelacija.Size = new System.Drawing.Size(332, 22);
            this.txtRelacija.TabIndex = 55;
            // 
            // lblArtikal
            // 
            this.lblArtikal.AutoSize = true;
            this.lblArtikal.Location = new System.Drawing.Point(20, 209);
            this.lblArtikal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArtikal.Name = "lblArtikal";
            this.lblArtikal.Size = new System.Drawing.Size(44, 16);
            this.lblArtikal.TabIndex = 480;
            this.lblArtikal.Text = "Artikal";
            // 
            // txtPrilozenaDokumenta
            // 
            this.txtPrilozenaDokumenta.Location = new System.Drawing.Point(194, 143);
            this.txtPrilozenaDokumenta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrilozenaDokumenta.Name = "txtPrilozenaDokumenta";
            this.txtPrilozenaDokumenta.ReadOnly = true;
            this.txtPrilozenaDokumenta.Size = new System.Drawing.Size(332, 22);
            this.txtPrilozenaDokumenta.TabIndex = 40;
            // 
            // txtKamioner
            // 
            this.txtKamioner.Location = new System.Drawing.Point(194, 114);
            this.txtKamioner.Margin = new System.Windows.Forms.Padding(4);
            this.txtKamioner.Name = "txtKamioner";
            this.txtKamioner.ReadOnly = true;
            this.txtKamioner.Size = new System.Drawing.Size(332, 22);
            this.txtKamioner.TabIndex = 35;
            // 
            // lblCena
            // 
            this.lblCena.AutoSize = true;
            this.lblCena.Location = new System.Drawing.Point(18, 270);
            this.lblCena.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCena.Name = "lblCena";
            this.lblCena.Size = new System.Drawing.Size(102, 16);
            this.lblCena.TabIndex = 476;
            this.lblCena.Text = "Cena transporta";
            // 
            // txtBrojUlazneFakture
            // 
            this.txtBrojUlazneFakture.Location = new System.Drawing.Point(194, 302);
            this.txtBrojUlazneFakture.Margin = new System.Windows.Forms.Padding(4);
            this.txtBrojUlazneFakture.Name = "txtBrojUlazneFakture";
            this.txtBrojUlazneFakture.ReadOnly = true;
            this.txtBrojUlazneFakture.Size = new System.Drawing.Size(332, 22);
            this.txtBrojUlazneFakture.TabIndex = 65;
            // 
            // lblPrilozenaDokumenta
            // 
            this.lblPrilozenaDokumenta.AutoSize = true;
            this.lblPrilozenaDokumenta.Location = new System.Drawing.Point(20, 146);
            this.lblPrilozenaDokumenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrilozenaDokumenta.Name = "lblPrilozenaDokumenta";
            this.lblPrilozenaDokumenta.Size = new System.Drawing.Size(133, 16);
            this.lblPrilozenaDokumenta.TabIndex = 470;
            this.lblPrilozenaDokumenta.Text = "Priložena dokumenta";
            // 
            // lblDatumIzlazneFakture
            // 
            this.lblDatumIzlazneFakture.AutoSize = true;
            this.lblDatumIzlazneFakture.Location = new System.Drawing.Point(19, 55);
            this.lblDatumIzlazneFakture.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatumIzlazneFakture.Name = "lblDatumIzlazneFakture";
            this.lblDatumIzlazneFakture.Size = new System.Drawing.Size(133, 16);
            this.lblDatumIzlazneFakture.TabIndex = 468;
            this.lblDatumIzlazneFakture.Text = "Datum izlazne fakture";
            // 
            // txtCenaTransporta
            // 
            this.txtCenaTransporta.Location = new System.Drawing.Point(194, 270);
            this.txtCenaTransporta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCenaTransporta.Name = "txtCenaTransporta";
            this.txtCenaTransporta.ReadOnly = true;
            this.txtCenaTransporta.Size = new System.Drawing.Size(332, 22);
            this.txtCenaTransporta.TabIndex = 60;
            // 
            // lblBrojUlazneFakture
            // 
            this.lblBrojUlazneFakture.AutoSize = true;
            this.lblBrojUlazneFakture.Location = new System.Drawing.Point(19, 302);
            this.lblBrojUlazneFakture.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBrojUlazneFakture.Name = "lblBrojUlazneFakture";
            this.lblBrojUlazneFakture.Size = new System.Drawing.Size(116, 16);
            this.lblBrojUlazneFakture.TabIndex = 478;
            this.lblBrojUlazneFakture.Text = "Broj ulazne fakture";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.detaljiToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 28);
            // 
            // detaljiToolStripMenuItem
            // 
            this.detaljiToolStripMenuItem.Name = "detaljiToolStripMenuItem";
            this.detaljiToolStripMenuItem.Size = new System.Drawing.Size(122, 24);
            this.detaljiToolStripMenuItem.Text = "Detalji";
            this.detaljiToolStripMenuItem.Click += new System.EventHandler(this.detaljiToolStripMenuItem_Click);
            // 
            // frmNalogZaFakturisanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 591);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmNalogZaFakturisanje";
            this.Text = "frmNalogZaFakturisanje";
            //this.Shown += new System.EventHandler(this.frmNalogZaFakturisanje_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmNalogZaFakturisanje_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtKontejnerID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblBooking;
        private System.Windows.Forms.Label lblBL;
        private System.Windows.Forms.Label lbBrojCNT;
        private System.Windows.Forms.Label lblContainerID;
        private System.Windows.Forms.TextBox txtBooking;
        private System.Windows.Forms.TextBox txtBL;
        private System.Windows.Forms.TextBox txtBrojKontejnera;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
        private System.Windows.Forms.TextBox txtNalogodavac;
        private System.Windows.Forms.Label lblDatumPregleda;
        private System.Windows.Forms.Label lblRelacija;
        private System.Windows.Forms.TextBox txtArtikal;
        private System.Windows.Forms.Label lblNalogodavac;
        private System.Windows.Forms.Label lblPrevoznik;
        private System.Windows.Forms.TextBox txtRelacija;
        private System.Windows.Forms.Label lblArtikal;
        private System.Windows.Forms.TextBox txtPrilozenaDokumenta;
        private System.Windows.Forms.TextBox txtKamioner;
        private System.Windows.Forms.Label lblCena;
        private System.Windows.Forms.TextBox txtBrojUlazneFakture;
        private System.Windows.Forms.Label lblPrilozenaDokumenta;
        private System.Windows.Forms.Label lblDatumIzlazneFakture;
        private System.Windows.Forms.TextBox txtCenaTransporta;
        private System.Windows.Forms.Label lblBrojUlazneFakture;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.DateTimePicker dtpPregleda;
        private System.Windows.Forms.DateTimePicker dtpDatumIzlazneFakture;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detaljiToolStripMenuItem;
        private System.Windows.Forms.Button btnPotvrdi;
        private System.Windows.Forms.Button btnIzadjiBezPromena;
        private System.Windows.Forms.Button btnBezIzlazneFakture;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNalog;
        private System.Windows.Forms.TextBox txtBrojlzlazneFakture;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnObjedinjenaDokumentaNalogID;
        private System.Windows.Forms.Button btnObjedinjenaDokumentaIFaktura;
    }
}


     