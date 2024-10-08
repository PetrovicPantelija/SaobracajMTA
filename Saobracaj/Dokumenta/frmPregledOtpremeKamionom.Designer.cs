namespace TrackModal.Dokumeta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledOtpremeKamionom));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
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
            this.panelLeget = new System.Windows.Forms.Panel();
            this.chkUvoz = new System.Windows.Forms.CheckBox();
            this.chkIzvoz = new System.Windows.Forms.CheckBox();
            this.chkPlatforma = new System.Windows.Forms.CheckBox();
            this.chkTerminal = new System.Windows.Forms.CheckBox();
            this.chkCirada = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelLeget.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton7,
            this.toolStripButton4,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1103, 27);
            this.toolStrip1.TabIndex = 129;
            this.toolStrip1.Text = "Pošalji mail infrastrukturi";
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
            this.txtSifra.Location = new System.Drawing.Point(23, 61);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(71, 20);
            this.txtSifra.TabIndex = 133;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(20, 42);
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
            this.dataGridView1.Location = new System.Drawing.Point(24, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1067, 231);
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
            this.chkKamionom.Location = new System.Drawing.Point(23, 100);
            this.chkKamionom.Name = "chkKamionom";
            this.chkKamionom.Size = new System.Drawing.Size(87, 20);
            this.chkKamionom.TabIndex = 197;
            this.chkKamionom.Text = "Kamionom";
            this.chkKamionom.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(729, 61);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 22);
            this.button3.TabIndex = 237;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(530, 61);
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
            this.label5.Location = new System.Drawing.Point(569, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 234;
            this.label5.Text = "Broj kontejnera:";
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBrojKontejnera.Location = new System.Drawing.Point(572, 62);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.Size = new System.Drawing.Size(151, 20);
            this.txtBrojKontejnera.TabIndex = 235;
            // 
            // txtBukingBrodar
            // 
            this.txtBukingBrodar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtBukingBrodar.Location = new System.Drawing.Point(337, 62);
            this.txtBukingBrodar.Name = "txtBukingBrodar";
            this.txtBukingBrodar.Size = new System.Drawing.Size(187, 20);
            this.txtBukingBrodar.TabIndex = 232;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(334, 43);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 13);
            this.label27.TabIndex = 233;
            this.label27.Text = "Buking brodar:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(292, 61);
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
            this.txtRegBrKamiona.Location = new System.Drawing.Point(101, 61);
            this.txtRegBrKamiona.Name = "txtRegBrKamiona";
            this.txtRegBrKamiona.Size = new System.Drawing.Size(185, 20);
            this.txtRegBrKamiona.TabIndex = 229;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Location = new System.Drawing.Point(98, 42);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(72, 13);
            this.label30.TabIndex = 230;
            this.label30.Text = "Reg br vozila:";
            // 
            // panelLeget
            // 
            this.panelLeget.Controls.Add(this.chkUvoz);
            this.panelLeget.Controls.Add(this.chkIzvoz);
            this.panelLeget.Controls.Add(this.chkPlatforma);
            this.panelLeget.Controls.Add(this.chkTerminal);
            this.panelLeget.Controls.Add(this.chkCirada);
            this.panelLeget.Location = new System.Drawing.Point(770, 43);
            this.panelLeget.Name = "panelLeget";
            this.panelLeget.Size = new System.Drawing.Size(321, 60);
            this.panelLeget.TabIndex = 474;
            this.panelLeget.Visible = false;
            // 
            // chkUvoz
            // 
            this.chkUvoz.AutoSize = true;
            this.chkUvoz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.chkUvoz.Checked = true;
            this.chkUvoz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUvoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkUvoz.ForeColor = System.Drawing.Color.Transparent;
            this.chkUvoz.Location = new System.Drawing.Point(25, 11);
            this.chkUvoz.Name = "chkUvoz";
            this.chkUvoz.Size = new System.Drawing.Size(51, 17);
            this.chkUvoz.TabIndex = 467;
            this.chkUvoz.Text = "Uvoz";
            this.chkUvoz.UseVisualStyleBackColor = false;
            // 
            // chkIzvoz
            // 
            this.chkIzvoz.AutoSize = true;
            this.chkIzvoz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.chkIzvoz.Checked = true;
            this.chkIzvoz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIzvoz.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkIzvoz.ForeColor = System.Drawing.Color.Transparent;
            this.chkIzvoz.Location = new System.Drawing.Point(25, 35);
            this.chkIzvoz.Name = "chkIzvoz";
            this.chkIzvoz.Size = new System.Drawing.Size(51, 17);
            this.chkIzvoz.TabIndex = 470;
            this.chkIzvoz.Text = "Izvoz";
            this.chkIzvoz.UseVisualStyleBackColor = false;
            // 
            // chkPlatforma
            // 
            this.chkPlatforma.AutoSize = true;
            this.chkPlatforma.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.chkPlatforma.Checked = true;
            this.chkPlatforma.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPlatforma.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkPlatforma.ForeColor = System.Drawing.Color.Transparent;
            this.chkPlatforma.Location = new System.Drawing.Point(199, 35);
            this.chkPlatforma.Name = "chkPlatforma";
            this.chkPlatforma.Size = new System.Drawing.Size(70, 17);
            this.chkPlatforma.TabIndex = 469;
            this.chkPlatforma.Text = "Platforma";
            this.chkPlatforma.UseVisualStyleBackColor = false;
            // 
            // chkTerminal
            // 
            this.chkTerminal.AutoSize = true;
            this.chkTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.chkTerminal.Checked = true;
            this.chkTerminal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkTerminal.ForeColor = System.Drawing.Color.Transparent;
            this.chkTerminal.Location = new System.Drawing.Point(103, 35);
            this.chkTerminal.Name = "chkTerminal";
            this.chkTerminal.Size = new System.Drawing.Size(66, 17);
            this.chkTerminal.TabIndex = 471;
            this.chkTerminal.Text = "Terminal";
            this.chkTerminal.UseVisualStyleBackColor = false;
            // 
            // chkCirada
            // 
            this.chkCirada.AutoSize = true;
            this.chkCirada.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.chkCirada.Checked = true;
            this.chkCirada.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCirada.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkCirada.ForeColor = System.Drawing.Color.Transparent;
            this.chkCirada.Location = new System.Drawing.Point(199, 11);
            this.chkCirada.Name = "chkCirada";
            this.chkCirada.Size = new System.Drawing.Size(56, 17);
            this.chkCirada.TabIndex = 468;
            this.chkCirada.Text = "Cirada";
            this.chkCirada.UseVisualStyleBackColor = false;
            // 
            // frmPregledOtpremeKamionom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1103, 369);
            this.Controls.Add(this.panelLeget);
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
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPregledOtpremeKamionom";
            this.Text = "Pregled otpreme kamionom";
            this.Load += new System.EventHandler(this.frmPregledOtpremeKamionom_Load);
            this.Click += new System.EventHandler(this.frmPregledOtpreme_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelLeget.ResumeLayout(false);
            this.panelLeget.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
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
        private System.Windows.Forms.Panel panelLeget;
        private System.Windows.Forms.CheckBox chkUvoz;
        private System.Windows.Forms.CheckBox chkIzvoz;
        private System.Windows.Forms.CheckBox chkPlatforma;
        private System.Windows.Forms.CheckBox chkTerminal;
        private System.Windows.Forms.CheckBox chkCirada;
    }
}