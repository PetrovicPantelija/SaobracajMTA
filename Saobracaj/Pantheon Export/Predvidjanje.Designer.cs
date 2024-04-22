namespace Saobracaj.Pantheon_Export
{
    partial class Predvidjanje
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Predvidjanje));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtIDPredvidjanja = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cboSubjekt = new System.Windows.Forms.ComboBox();
            this.cboNosilacTroska = new System.Windows.Forms.ComboBox();
            this.cboValuta = new System.Windows.Forms.ComboBox();
            this.cboOdeljenje = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboPredvidjanjeIDFilter = new System.Windows.Forms.ComboBox();
            this.btnPredvidjanjeFilter = new System.Windows.Forms.Button();
            this.btnNTNazivFilter = new System.Windows.Forms.Button();
            this.txtFilterNazivNT = new System.Windows.Forms.TextBox();
            this.btnNTFilter = new System.Windows.Forms.Button();
            this.txtFilterNT = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.txtPredvidjanje = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.txtIznos = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.cboIdent = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboJM = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtKolicina = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtKurs = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIznos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKolicina)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKurs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 130);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1614, 626);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1638, 27);
            this.toolStrip1.TabIndex = 198;
            this.toolStrip1.Text = "Osveži";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(108, 24);
            this.toolStripButton2.Text = "Sva predvidjanja";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripButton3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(108, 24);
            this.toolStripButton3.Text = "Export Pantheon";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(337, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 199;
            this.label1.Text = "Datum";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 199;
            this.label2.Text = "ID Predvidjanja";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(577, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 199;
            this.label3.Text = "Subjekt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(894, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 199;
            this.label4.Text = "Nosilac troška";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 199;
            this.label5.Text = "Odeljenje";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(951, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 199;
            this.label7.Text = "Iznos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1074, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 199;
            this.label8.Text = "Valuta";
            // 
            // txtIDPredvidjanja
            // 
            this.txtIDPredvidjanja.Enabled = false;
            this.txtIDPredvidjanja.Location = new System.Drawing.Point(97, 53);
            this.txtIDPredvidjanja.Name = "txtIDPredvidjanja";
            this.txtIDPredvidjanja.Size = new System.Drawing.Size(46, 20);
            this.txtIDPredvidjanja.TabIndex = 200;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(306, 51);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(114, 20);
            this.dateTimePicker1.TabIndex = 201;
            // 
            // cboSubjekt
            // 
            this.cboSubjekt.FormattingEnabled = true;
            this.cboSubjekt.Location = new System.Drawing.Point(426, 50);
            this.cboSubjekt.Name = "cboSubjekt";
            this.cboSubjekt.Size = new System.Drawing.Size(386, 21);
            this.cboSubjekt.TabIndex = 202;
            // 
            // cboNosilacTroska
            // 
            this.cboNosilacTroska.FormattingEnabled = true;
            this.cboNosilacTroska.Location = new System.Drawing.Point(818, 50);
            this.cboNosilacTroska.Name = "cboNosilacTroska";
            this.cboNosilacTroska.Size = new System.Drawing.Size(273, 21);
            this.cboNosilacTroska.TabIndex = 202;
            // 
            // cboValuta
            // 
            this.cboValuta.FormattingEnabled = true;
            this.cboValuta.Location = new System.Drawing.Point(1041, 102);
            this.cboValuta.Name = "cboValuta";
            this.cboValuta.Size = new System.Drawing.Size(119, 21);
            this.cboValuta.TabIndex = 202;
            // 
            // cboOdeljenje
            // 
            this.cboOdeljenje.FormattingEnabled = true;
            this.cboOdeljenje.Location = new System.Drawing.Point(12, 102);
            this.cboOdeljenje.Name = "cboOdeljenje";
            this.cboOdeljenje.Size = new System.Drawing.Size(222, 21);
            this.cboOdeljenje.TabIndex = 203;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.cboPredvidjanjeIDFilter);
            this.panel1.Controls.Add(this.btnPredvidjanjeFilter);
            this.panel1.Controls.Add(this.btnNTNazivFilter);
            this.panel1.Controls.Add(this.txtFilterNazivNT);
            this.panel1.Controls.Add(this.btnNTFilter);
            this.panel1.Controls.Add(this.txtFilterNT);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Location = new System.Drawing.Point(12, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1614, 679);
            this.panel1.TabIndex = 204;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(1115, 9);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(99, 30);
            this.btnRefresh.TabIndex = 14;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cboPredvidjanjeIDFilter
            // 
            this.cboPredvidjanjeIDFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cboPredvidjanjeIDFilter.FormattingEnabled = true;
            this.cboPredvidjanjeIDFilter.Location = new System.Drawing.Point(869, 12);
            this.cboPredvidjanjeIDFilter.Name = "cboPredvidjanjeIDFilter";
            this.cboPredvidjanjeIDFilter.Size = new System.Drawing.Size(187, 23);
            this.cboPredvidjanjeIDFilter.TabIndex = 13;
            // 
            // btnPredvidjanjeFilter
            // 
            this.btnPredvidjanjeFilter.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnPredvidjanjeFilter.Location = new System.Drawing.Point(1059, 13);
            this.btnPredvidjanjeFilter.Name = "btnPredvidjanjeFilter";
            this.btnPredvidjanjeFilter.Size = new System.Drawing.Size(20, 21);
            this.btnPredvidjanjeFilter.TabIndex = 12;
            this.btnPredvidjanjeFilter.UseVisualStyleBackColor = false;
            this.btnPredvidjanjeFilter.Click += new System.EventHandler(this.btnPredvidjanjeFilter_Click);
            // 
            // btnNTNazivFilter
            // 
            this.btnNTNazivFilter.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnNTNazivFilter.Location = new System.Drawing.Point(654, 11);
            this.btnNTNazivFilter.Name = "btnNTNazivFilter";
            this.btnNTNazivFilter.Size = new System.Drawing.Size(20, 21);
            this.btnNTNazivFilter.TabIndex = 12;
            this.btnNTNazivFilter.UseVisualStyleBackColor = false;
            this.btnNTNazivFilter.Click += new System.EventHandler(this.btnNTNazivFilter_Click);
            // 
            // txtFilterNazivNT
            // 
            this.txtFilterNazivNT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterNazivNT.Location = new System.Drawing.Point(434, 10);
            this.txtFilterNazivNT.Name = "txtFilterNazivNT";
            this.txtFilterNazivNT.Size = new System.Drawing.Size(214, 22);
            this.txtFilterNazivNT.TabIndex = 11;
            // 
            // btnNTFilter
            // 
            this.btnNTFilter.BackColor = System.Drawing.Color.NavajoWhite;
            this.btnNTFilter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNTFilter.Location = new System.Drawing.Point(257, 12);
            this.btnNTFilter.Name = "btnNTFilter";
            this.btnNTFilter.Size = new System.Drawing.Size(20, 20);
            this.btnNTFilter.TabIndex = 10;
            this.btnNTFilter.UseVisualStyleBackColor = false;
            this.btnNTFilter.Click += new System.EventHandler(this.btnNTFilter_Click);
            // 
            // txtFilterNT
            // 
            this.txtFilterNT.Location = new System.Drawing.Point(138, 12);
            this.txtFilterNT.Name = "txtFilterNT";
            this.txtFilterNT.Size = new System.Drawing.Size(113, 20);
            this.txtFilterNT.TabIndex = 9;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label18.Location = new System.Drawing.Point(696, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(167, 16);
            this.label18.TabIndex = 7;
            this.label18.Text = "Pretraži po predvidjanjuID :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label16.Location = new System.Drawing.Point(291, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(137, 16);
            this.label16.TabIndex = 7;
            this.label16.Text = "Pretraži po nazivu NT:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label17.Location = new System.Drawing.Point(8, 16);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(124, 16);
            this.label17.TabIndex = 8;
            this.label17.Text = "Pretraži po nosiocu:";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(1428, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "Vrati na status 0";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.NavajoWhite;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1528, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Nazad";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(3, 48);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1608, 628);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // txtPredvidjanje
            // 
            this.txtPredvidjanje.Enabled = false;
            this.txtPredvidjanje.Location = new System.Drawing.Point(180, 53);
            this.txtPredvidjanje.Name = "txtPredvidjanje";
            this.txtPredvidjanje.Size = new System.Drawing.Size(111, 20);
            this.txtPredvidjanje.TabIndex = 205;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 199;
            this.label6.Text = "Naziv Predvidjanja";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DodgerBlue;
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(1304, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 206;
            this.button2.Text = "Dodaj novu poziciju";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtIznos
            // 
            this.txtIznos.DecimalPlaces = 4;
            this.txtIznos.Location = new System.Drawing.Point(928, 103);
            this.txtIznos.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtIznos.Name = "txtIznos";
            this.txtIznos.Size = new System.Drawing.Size(90, 20);
            this.txtIznos.TabIndex = 211;
            this.txtIznos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtIznos.ValueChanged += new System.EventHandler(this.txtIznos_ValueChanged);
            this.txtIznos.Leave += new System.EventHandler(this.txtIznos_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 13);
            this.label9.TabIndex = 212;
            this.label9.Text = "ID";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(12, 53);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(57, 20);
            this.txtID.TabIndex = 213;
            // 
            // cboIdent
            // 
            this.cboIdent.FormattingEnabled = true;
            this.cboIdent.Location = new System.Drawing.Point(240, 102);
            this.cboIdent.Name = "cboIdent";
            this.cboIdent.Size = new System.Drawing.Size(324, 21);
            this.cboIdent.TabIndex = 214;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(389, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 215;
            this.label10.Text = "Ident";
            // 
            // cboJM
            // 
            this.cboJM.FormattingEnabled = true;
            this.cboJM.Location = new System.Drawing.Point(686, 103);
            this.cboJM.Name = "cboJM";
            this.cboJM.Size = new System.Drawing.Size(112, 21);
            this.cboJM.TabIndex = 216;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(728, 86);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(21, 13);
            this.label11.TabIndex = 215;
            this.label11.Text = "JM";
            // 
            // txtKolicina
            // 
            this.txtKolicina.DecimalPlaces = 2;
            this.txtKolicina.Location = new System.Drawing.Point(580, 104);
            this.txtKolicina.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtKolicina.Name = "txtKolicina";
            this.txtKolicina.Size = new System.Drawing.Size(90, 20);
            this.txtKolicina.TabIndex = 211;
            this.txtKolicina.ValueChanged += new System.EventHandler(this.txtKolicina_ValueChanged);
            this.txtKolicina.Leave += new System.EventHandler(this.txtKolicina_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(601, 86);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 199;
            this.label12.Text = "Količina";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(1108, 50);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(308, 33);
            this.txtNapomena.TabIndex = 217;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1230, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 13);
            this.label13.TabIndex = 199;
            this.label13.Text = "Napomena";
            // 
            // txtKurs
            // 
            this.txtKurs.DecimalPlaces = 4;
            this.txtKurs.Location = new System.Drawing.Point(1186, 104);
            this.txtKurs.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.txtKurs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtKurs.Name = "txtKurs";
            this.txtKurs.Size = new System.Drawing.Size(90, 20);
            this.txtKurs.TabIndex = 211;
            this.txtKurs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1208, 88);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(28, 13);
            this.label14.TabIndex = 199;
            this.label14.Text = "Kurs";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 4;
            this.numericUpDown1.Location = new System.Drawing.Point(818, 104);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 211;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDown1.Leave += new System.EventHandler(this.numericUpDown1_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(829, 86);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 199;
            this.label15.Text = "Jedinična cena";
            // 
            // Predvidjanje
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1638, 768);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboOdeljenje);
            this.Controls.Add(this.cboValuta);
            this.Controls.Add(this.cboNosilacTroska);
            this.Controls.Add(this.cboSubjekt);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtIDPredvidjanja);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtPredvidjanje);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtKolicina);
            this.Controls.Add(this.txtKurs);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.txtIznos);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cboJM);
            this.Controls.Add(this.cboIdent);
            this.Controls.Add(this.label10);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Predvidjanje";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Predvidjanje";
            this.Load += new System.EventHandler(this.Predvidjanje_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtIznos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKolicina)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKurs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIDPredvidjanja;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ComboBox cboSubjekt;
        private System.Windows.Forms.ComboBox cboNosilacTroska;
        private System.Windows.Forms.ComboBox cboValuta;
        private System.Windows.Forms.ComboBox cboOdeljenje;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtPredvidjanje;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown txtIznos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ComboBox cboIdent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboJM;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown txtKolicina;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.NumericUpDown txtKurs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnNTNazivFilter;
        private System.Windows.Forms.TextBox txtFilterNazivNT;
        private System.Windows.Forms.Button btnNTFilter;
        private System.Windows.Forms.TextBox txtFilterNT;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboPredvidjanjeIDFilter;
        private System.Windows.Forms.Button btnPredvidjanjeFilter;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnRefresh;
    }
}