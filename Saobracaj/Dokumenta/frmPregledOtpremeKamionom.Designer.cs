﻿namespace TrackModal.Dokumeta
{
    partial class frmPregledOtpremeKamionom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledOtpremeKamionom));
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkKamionom = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBrojKontejnera = new System.Windows.Forms.TextBox();
            this.txtBukingBrodar = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtRegBrKamiona = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.chkUvoz = new System.Windows.Forms.CheckBox();
            this.chkIzvoz = new System.Windows.Forms.CheckBox();
            this.chkPlatforma = new System.Windows.Forms.CheckBox();
            this.chkTerminal = new System.Windows.Forms.CheckBox();
            this.chkCirada = new System.Windows.Forms.CheckBox();
            this.button8 = new System.Windows.Forms.Button();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panelLeget = new System.Windows.Forms.Panel();
            this.meniHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panelLeget.SuspendLayout();
            this.SuspendLayout();
            // 
            // meniHeader
            // 
            this.meniHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.meniHeader.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.meniHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton7,
            this.toolStripButton4,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton5,
            this.toolStripButton6});
            this.meniHeader.Location = new System.Drawing.Point(0, 0);
            this.meniHeader.Name = "meniHeader";
            this.meniHeader.Size = new System.Drawing.Size(1178, 27);
            this.meniHeader.TabIndex = 129;
            this.meniHeader.Text = "Pošalji mail infrastrukturi";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton7.ForeColor = System.Drawing.Color.White;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(120, 24);
            this.toolStripButton7.Text = "Otvori kamion";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton4.ForeColor = System.Drawing.Color.White;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(159, 24);
            this.toolStripButton4.Text = "Otvori kamion UVOZ";
            this.toolStripButton4.Visible = false;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(161, 24);
            this.toolStripButton1.Text = "Otvori kamion IZVOZ";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton2.ForeColor = System.Drawing.Color.White;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(73, 24);
            this.toolStripButton2.Text = "Osveži";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton3.ForeColor = System.Drawing.Color.White;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(110, 24);
            this.toolStripButton3.Text = "Novi kamion";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton5.ForeColor = System.Drawing.Color.White;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(133, 24);
            this.toolStripButton5.Text = "Otprema najava";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton6.ForeColor = System.Drawing.Color.White;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(161, 24);
            this.toolStripButton6.Text = "Otpremljeni kamioni";
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSifra.Location = new System.Drawing.Point(23, 94);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(82, 20);
            this.txtSifra.TabIndex = 133;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 134;
            this.label1.Text = "Šifra:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(24, 192);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1142, 457);
            this.dataGridView1.TabIndex = 132;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged_2);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // chkKamionom
            // 
            this.chkKamionom.AutoSize = true;
            this.chkKamionom.Checked = true;
            this.chkKamionom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKamionom.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkKamionom.ForeColor = System.Drawing.Color.Black;
            this.chkKamionom.Location = new System.Drawing.Point(23, 127);
            this.chkKamionom.Name = "chkKamionom";
            this.chkKamionom.Size = new System.Drawing.Size(87, 20);
            this.chkKamionom.TabIndex = 197;
            this.chkKamionom.Text = "Kamionom";
            this.chkKamionom.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(711, 144);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 22);
            this.button3.TabIndex = 237;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(711, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 23);
            this.button2.TabIndex = 236;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(467, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 234;
            this.label5.Text = "Broj kontejnera:";
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBrojKontejnera.Location = new System.Drawing.Point(470, 146);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.Size = new System.Drawing.Size(235, 20);
            this.txtBrojKontejnera.TabIndex = 235;
            // 
            // txtBukingBrodar
            // 
            this.txtBukingBrodar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBukingBrodar.Location = new System.Drawing.Point(470, 95);
            this.txtBukingBrodar.Name = "txtBukingBrodar";
            this.txtBukingBrodar.Size = new System.Drawing.Size(235, 20);
            this.txtBukingBrodar.TabIndex = 232;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(467, 76);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 13);
            this.label27.TabIndex = 233;
            this.label27.Text = "Buking brodar:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(421, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 22);
            this.button1.TabIndex = 231;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtRegBrKamiona
            // 
            this.txtRegBrKamiona.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtRegBrKamiona.Location = new System.Drawing.Point(176, 94);
            this.txtRegBrKamiona.Name = "txtRegBrKamiona";
            this.txtRegBrKamiona.Size = new System.Drawing.Size(235, 20);
            this.txtRegBrKamiona.TabIndex = 229;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(173, 75);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 13);
            this.label30.TabIndex = 230;
            this.label30.Text = "Reg br vozila:";
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel7);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 27);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1178, 33);
            this.panelHeader.TabIndex = 475;
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel7.Controls.Add(this.button8);
            this.panel7.Controls.Add(this.button9);
            this.panel7.Controls.Add(this.button7);
            this.panel7.Controls.Add(this.button6);
            this.panel7.Controls.Add(this.button5);
            this.panel7.Controls.Add(this.button4);
            this.panel7.Controls.Add(this.button24);
            this.panel7.Location = new System.Drawing.Point(3, 1);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1138, 31);
            this.panel7.TabIndex = 6;
            // 
            // button9
            // 
            this.button9.AutoSize = true;
            this.button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button9.Dock = System.Windows.Forms.DockStyle.Left;
            this.button9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button9.Location = new System.Drawing.Point(659, 0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(168, 31);
            this.button9.TabIndex = 23;
            this.button9.Text = "Otvori  kamionom";
            this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // button7
            // 
            this.button7.AutoSize = true;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.Dock = System.Windows.Forms.DockStyle.Left;
            this.button7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button7.Location = new System.Drawing.Point(598, 0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(61, 31);
            this.button7.TabIndex = 21;
            this.button7.Text = "Najava";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // button6
            // 
            this.button6.AutoSize = true;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Dock = System.Windows.Forms.DockStyle.Left;
            this.button6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button6.Location = new System.Drawing.Point(503, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(95, 31);
            this.button6.TabIndex = 20;
            this.button6.Text = "Novi kamion";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // button5
            // 
            this.button5.AutoSize = true;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.Dock = System.Windows.Forms.DockStyle.Left;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button5.Location = new System.Drawing.Point(443, 0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(60, 31);
            this.button5.TabIndex = 19;
            this.button5.Text = "Osveži";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button4.Location = new System.Drawing.Point(220, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(223, 31);
            this.button4.TabIndex = 18;
            this.button4.Text = "Otvori prijem kamionom - IZVOZ";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // button24
            // 
            this.button24.AutoSize = true;
            this.button24.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button24.Dock = System.Windows.Forms.DockStyle.Left;
            this.button24.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button24.FlatAppearance.BorderSize = 0;
            this.button24.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button24.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button24.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button24.Location = new System.Drawing.Point(0, 0);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(220, 31);
            this.button24.TabIndex = 17;
            this.button24.Text = "Otvori prijem kamionom - UVOZ";
            this.button24.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // chkUvoz
            // 
            this.chkUvoz.AutoSize = true;
            this.chkUvoz.BackColor = System.Drawing.Color.Transparent;
            this.chkUvoz.Checked = true;
            this.chkUvoz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUvoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkUvoz.ForeColor = System.Drawing.Color.Black;
            this.chkUvoz.Location = new System.Drawing.Point(46, 25);
            this.chkUvoz.Name = "chkUvoz";
            this.chkUvoz.Size = new System.Drawing.Size(51, 17);
            this.chkUvoz.TabIndex = 476;
            this.chkUvoz.Text = "Uvoz";
            this.chkUvoz.UseVisualStyleBackColor = false;
            // 
            // chkIzvoz
            // 
            this.chkIzvoz.AutoSize = true;
            this.chkIzvoz.BackColor = System.Drawing.Color.Transparent;
            this.chkIzvoz.Checked = true;
            this.chkIzvoz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIzvoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkIzvoz.ForeColor = System.Drawing.Color.Black;
            this.chkIzvoz.Location = new System.Drawing.Point(46, 49);
            this.chkIzvoz.Name = "chkIzvoz";
            this.chkIzvoz.Size = new System.Drawing.Size(51, 17);
            this.chkIzvoz.TabIndex = 479;
            this.chkIzvoz.Text = "Izvoz";
            this.chkIzvoz.UseVisualStyleBackColor = false;
            // 
            // chkPlatforma
            // 
            this.chkPlatforma.AutoSize = true;
            this.chkPlatforma.BackColor = System.Drawing.Color.Transparent;
            this.chkPlatforma.Checked = true;
            this.chkPlatforma.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlatforma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkPlatforma.ForeColor = System.Drawing.Color.Black;
            this.chkPlatforma.Location = new System.Drawing.Point(220, 49);
            this.chkPlatforma.Name = "chkPlatforma";
            this.chkPlatforma.Size = new System.Drawing.Size(70, 17);
            this.chkPlatforma.TabIndex = 478;
            this.chkPlatforma.Text = "Platforma";
            this.chkPlatforma.UseVisualStyleBackColor = false;
            // 
            // chkTerminal
            // 
            this.chkTerminal.AutoSize = true;
            this.chkTerminal.BackColor = System.Drawing.Color.Transparent;
            this.chkTerminal.Checked = true;
            this.chkTerminal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkTerminal.ForeColor = System.Drawing.Color.Black;
            this.chkTerminal.Location = new System.Drawing.Point(124, 49);
            this.chkTerminal.Name = "chkTerminal";
            this.chkTerminal.Size = new System.Drawing.Size(66, 17);
            this.chkTerminal.TabIndex = 480;
            this.chkTerminal.Text = "Terminal";
            this.chkTerminal.UseVisualStyleBackColor = false;
            // 
            // chkCirada
            // 
            this.chkCirada.AutoSize = true;
            this.chkCirada.BackColor = System.Drawing.Color.Transparent;
            this.chkCirada.Checked = true;
            this.chkCirada.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCirada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkCirada.ForeColor = System.Drawing.Color.Black;
            this.chkCirada.Location = new System.Drawing.Point(220, 25);
            this.chkCirada.Name = "chkCirada";
            this.chkCirada.Size = new System.Drawing.Size(56, 17);
            this.chkCirada.TabIndex = 477;
            this.chkCirada.Text = "Cirada";
            this.chkCirada.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.AutoSize = true;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button8.Dock = System.Windows.Forms.DockStyle.Left;
            this.button8.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button8.Location = new System.Drawing.Point(827, 0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(55, 31);
            this.button8.TabIndex = 24;
            this.button8.Text = "Otvori";
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // panelLeget
            // 
            this.panelLeget.BackColor = System.Drawing.Color.White;
            this.panelLeget.Controls.Add(this.chkCirada);
            this.panelLeget.Controls.Add(this.chkUvoz);
            this.panelLeget.Controls.Add(this.chkTerminal);
            this.panelLeget.Controls.Add(this.chkIzvoz);
            this.panelLeget.Controls.Add(this.chkPlatforma);
            this.panelLeget.Location = new System.Drawing.Point(788, 76);
            this.panelLeget.Name = "panelLeget";
            this.panelLeget.Size = new System.Drawing.Size(378, 100);
            this.panelLeget.TabIndex = 485;
            // 
            // frmPregledOtpremeKamionom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1178, 661);
            this.Controls.Add(this.panelLeget);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBrojKontejnera);
            this.Controls.Add(this.txtBukingBrodar);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtRegBrKamiona);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.chkKamionom);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.meniHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPregledOtpremeKamionom";
            this.Text = "Pregled otpreme kamionom";
            this.Load += new System.EventHandler(this.frmPregledOtpremeKamionom_Load);
            this.Click += new System.EventHandler(this.frmPregledOtpreme_Load);
            this.meniHeader.ResumeLayout(false);
            this.meniHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panelLeget.ResumeLayout(false);
            this.panelLeget.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip meniHeader;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkKamionom;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBrojKontejnera;
        private System.Windows.Forms.TextBox txtBukingBrodar;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtRegBrKamiona;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.CheckBox chkUvoz;
        private System.Windows.Forms.CheckBox chkIzvoz;
        private System.Windows.Forms.CheckBox chkPlatforma;
        private System.Windows.Forms.CheckBox chkTerminal;
        private System.Windows.Forms.CheckBox chkCirada;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Panel panelLeget;
    }
}