namespace Saobracaj.Drumski
{
    partial class frmObradaUlaznihFaktura
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
            this.btnIzadjiBezPromena = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dtpPregleda = new System.Windows.Forms.DateTimePicker();
            this.dtpPrometa = new System.Windows.Forms.DateTimePicker();
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
            this.lblDatumPrometa = new System.Windows.Forms.Label();
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
            this.panel1.Size = new System.Drawing.Size(1213, 81);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(787, 35);
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
            this.lblBooking.Location = new System.Drawing.Point(549, 18);
            this.lblBooking.Name = "lblBooking";
            this.lblBooking.Size = new System.Drawing.Size(57, 16);
            this.lblBooking.TabIndex = 7;
            this.lblBooking.Text = "Booking";
            // 
            // lblBL
            // 
            this.lblBL.AutoSize = true;
            this.lblBL.Location = new System.Drawing.Point(395, 18);
            this.lblBL.Name = "lblBL";
            this.lblBL.Size = new System.Drawing.Size(23, 16);
            this.lblBL.TabIndex = 6;
            this.lblBL.Text = "BL";
            // 
            // lbBrojCNT
            // 
            this.lbBrojCNT.AutoSize = true;
            this.lbBrojCNT.Location = new System.Drawing.Point(184, 18);
            this.lbBrojCNT.Name = "lbBrojCNT";
            this.lbBrojCNT.Size = new System.Drawing.Size(62, 16);
            this.lbBrojCNT.TabIndex = 5;
            this.lbBrojCNT.Text = "Broj CNT";
            // 
            // lblContainerID
            // 
            this.lblContainerID.AutoSize = true;
            this.lblContainerID.Location = new System.Drawing.Point(10, 18);
            this.lblContainerID.Name = "lblContainerID";
            this.lblContainerID.Size = new System.Drawing.Size(79, 16);
            this.lblContainerID.TabIndex = 4;
            this.lblContainerID.Text = "Kontejner ID";
            // 
            // txtBooking
            // 
            this.txtBooking.Location = new System.Drawing.Point(552, 37);
            this.txtBooking.Name = "txtBooking";
            this.txtBooking.Size = new System.Drawing.Size(196, 22);
            this.txtBooking.TabIndex = 15;
            // 
            // txtBL
            // 
            this.txtBL.Location = new System.Drawing.Point(391, 37);
            this.txtBL.Name = "txtBL";
            this.txtBL.Size = new System.Drawing.Size(155, 22);
            this.txtBL.TabIndex = 10;
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Location = new System.Drawing.Point(187, 37);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.Size = new System.Drawing.Size(198, 22);
            this.txtBrojKontejnera.TabIndex = 5;
            // 
            // txtKontejnerID
            // 
            this.txtKontejnerID.Location = new System.Drawing.Point(8, 37);
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
            this.gridGroupingControl1.DoubleClick += new System.EventHandler(this.gridGroupingControl1_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btnIzadjiBezPromena);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.dtpPregleda);
            this.panel3.Controls.Add(this.dtpPrometa);
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
            this.panel3.Controls.Add(this.lblDatumPrometa);
            this.panel3.Controls.Add(this.txtCenaTransporta);
            this.panel3.Controls.Add(this.lblBrojUlazneFakture);
            this.panel3.Location = new System.Drawing.Point(4, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1213, 469);
            this.panel3.TabIndex = 2;
            // 
            // btnIzadjiBezPromena
            // 
            this.btnIzadjiBezPromena.Location = new System.Drawing.Point(372, 318);
            this.btnIzadjiBezPromena.Name = "btnIzadjiBezPromena";
            this.btnIzadjiBezPromena.Size = new System.Drawing.Size(154, 27);
            this.btnIzadjiBezPromena.TabIndex = 483;
            this.btnIzadjiBezPromena.Text = "Izađi bez promena";
            this.btnIzadjiBezPromena.UseVisualStyleBackColor = true;
            this.btnIzadjiBezPromena.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(194, 318);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(154, 27);
            this.button2.TabIndex = 70;
            this.button2.Text = "Potvrdi ulaznu fakturu";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // dtpPrometa
            // 
            this.dtpPrometa.CustomFormat = "dd.MM.yyyy";
            this.dtpPrometa.Enabled = false;
            this.dtpPrometa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrometa.Location = new System.Drawing.Point(194, 49);
            this.dtpPrometa.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpPrometa.Name = "dtpPrometa";
            this.dtpPrometa.Size = new System.Drawing.Size(192, 22);
            this.dtpPrometa.TabIndex = 30;
            this.dtpPrometa.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // txtNalogodavac
            // 
            this.txtNalogodavac.Location = new System.Drawing.Point(194, 142);
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
            this.lblRelacija.Location = new System.Drawing.Point(20, 206);
            this.lblRelacija.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRelacija.Name = "lblRelacija";
            this.lblRelacija.Size = new System.Drawing.Size(57, 16);
            this.lblRelacija.TabIndex = 474;
            this.lblRelacija.Text = "Relacija";
            // 
            // txtArtikal
            // 
            this.txtArtikal.Location = new System.Drawing.Point(194, 174);
            this.txtArtikal.Margin = new System.Windows.Forms.Padding(4);
            this.txtArtikal.Name = "txtArtikal";
            this.txtArtikal.ReadOnly = true;
            this.txtArtikal.Size = new System.Drawing.Size(332, 22);
            this.txtArtikal.TabIndex = 50;
            // 
            // lblNalogodavac
            // 
            this.lblNalogodavac.AutoSize = true;
            this.lblNalogodavac.Location = new System.Drawing.Point(19, 146);
            this.lblNalogodavac.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNalogodavac.Name = "lblNalogodavac";
            this.lblNalogodavac.Size = new System.Drawing.Size(90, 16);
            this.lblNalogodavac.TabIndex = 472;
            this.lblNalogodavac.Text = "Nalogodavac";
            // 
            // lblPrevoznik
            // 
            this.lblPrevoznik.AutoSize = true;
            this.lblPrevoznik.Location = new System.Drawing.Point(20, 82);
            this.lblPrevoznik.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrevoznik.Name = "lblPrevoznik";
            this.lblPrevoznik.Size = new System.Drawing.Size(133, 16);
            this.lblPrevoznik.TabIndex = 466;
            this.lblPrevoznik.Text = "Prevoznik / Kamioner";
            // 
            // txtRelacija
            // 
            this.txtRelacija.Location = new System.Drawing.Point(194, 206);
            this.txtRelacija.Margin = new System.Windows.Forms.Padding(4);
            this.txtRelacija.Name = "txtRelacija";
            this.txtRelacija.ReadOnly = true;
            this.txtRelacija.Size = new System.Drawing.Size(332, 22);
            this.txtRelacija.TabIndex = 55;
            // 
            // lblArtikal
            // 
            this.lblArtikal.AutoSize = true;
            this.lblArtikal.Location = new System.Drawing.Point(19, 178);
            this.lblArtikal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArtikal.Name = "lblArtikal";
            this.lblArtikal.Size = new System.Drawing.Size(44, 16);
            this.lblArtikal.TabIndex = 480;
            this.lblArtikal.Text = "Artikal";
            // 
            // txtPrilozenaDokumenta
            // 
            this.txtPrilozenaDokumenta.Location = new System.Drawing.Point(194, 111);
            this.txtPrilozenaDokumenta.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrilozenaDokumenta.Name = "txtPrilozenaDokumenta";
            this.txtPrilozenaDokumenta.ReadOnly = true;
            this.txtPrilozenaDokumenta.Size = new System.Drawing.Size(332, 22);
            this.txtPrilozenaDokumenta.TabIndex = 40;
            // 
            // txtKamioner
            // 
            this.txtKamioner.Location = new System.Drawing.Point(194, 82);
            this.txtKamioner.Margin = new System.Windows.Forms.Padding(4);
            this.txtKamioner.Name = "txtKamioner";
            this.txtKamioner.ReadOnly = true;
            this.txtKamioner.Size = new System.Drawing.Size(332, 22);
            this.txtKamioner.TabIndex = 35;
            // 
            // lblCena
            // 
            this.lblCena.AutoSize = true;
            this.lblCena.Location = new System.Drawing.Point(18, 238);
            this.lblCena.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCena.Name = "lblCena";
            this.lblCena.Size = new System.Drawing.Size(102, 16);
            this.lblCena.TabIndex = 476;
            this.lblCena.Text = "Cena transporta";
            // 
            // txtBrojUlazneFakture
            // 
            this.txtBrojUlazneFakture.Location = new System.Drawing.Point(194, 270);
            this.txtBrojUlazneFakture.Margin = new System.Windows.Forms.Padding(4);
            this.txtBrojUlazneFakture.Name = "txtBrojUlazneFakture";
            this.txtBrojUlazneFakture.Size = new System.Drawing.Size(332, 22);
            this.txtBrojUlazneFakture.TabIndex = 65;
            // 
            // lblPrilozenaDokumenta
            // 
            this.lblPrilozenaDokumenta.AutoSize = true;
            this.lblPrilozenaDokumenta.Location = new System.Drawing.Point(20, 114);
            this.lblPrilozenaDokumenta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrilozenaDokumenta.Name = "lblPrilozenaDokumenta";
            this.lblPrilozenaDokumenta.Size = new System.Drawing.Size(133, 16);
            this.lblPrilozenaDokumenta.TabIndex = 470;
            this.lblPrilozenaDokumenta.Text = "Priložena dokumenta";
            // 
            // lblDatumPrometa
            // 
            this.lblDatumPrometa.AutoSize = true;
            this.lblDatumPrometa.Location = new System.Drawing.Point(19, 55);
            this.lblDatumPrometa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatumPrometa.Name = "lblDatumPrometa";
            this.lblDatumPrometa.Size = new System.Drawing.Size(99, 16);
            this.lblDatumPrometa.TabIndex = 468;
            this.lblDatumPrometa.Text = "Datum prometa";
            // 
            // txtCenaTransporta
            // 
            this.txtCenaTransporta.Location = new System.Drawing.Point(194, 238);
            this.txtCenaTransporta.Margin = new System.Windows.Forms.Padding(4);
            this.txtCenaTransporta.Name = "txtCenaTransporta";
            this.txtCenaTransporta.ReadOnly = true;
            this.txtCenaTransporta.Size = new System.Drawing.Size(332, 22);
            this.txtCenaTransporta.TabIndex = 60;
            // 
            // lblBrojUlazneFakture
            // 
            this.lblBrojUlazneFakture.AutoSize = true;
            this.lblBrojUlazneFakture.Location = new System.Drawing.Point(19, 270);
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
            // frmObradaUlaznihFaktura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 591);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmObradaUlaznihFaktura";
            this.Text = "frmObradaUlaznihFaktura";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmObradaUlaznihFaktura_KeyDown);
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
        private System.Windows.Forms.Label lblDatumPrometa;
        private System.Windows.Forms.TextBox txtCenaTransporta;
        private System.Windows.Forms.Label lblBrojUlazneFakture;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.DateTimePicker dtpPregleda;
        private System.Windows.Forms.DateTimePicker dtpPrometa;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem detaljiToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnIzadjiBezPromena;
    }
}