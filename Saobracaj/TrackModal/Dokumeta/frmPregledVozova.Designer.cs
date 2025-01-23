namespace Testiranje.Dokumeta
{
    partial class frmPregledVozova
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledVozova));
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.mainHeder = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button26 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.meniHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // meniHeader
            // 
            this.meniHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.meniHeader.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.meniHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton4,
            this.toolStripButton5});
            this.meniHeader.Location = new System.Drawing.Point(0, 33);
            this.meniHeader.Name = "meniHeader";
            this.meniHeader.Size = new System.Drawing.Size(1049, 27);
            this.meniHeader.TabIndex = 129;
            this.meniHeader.Text = "Pošalji mail infrastrukturi";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(113, 24);
            this.toolStripButton1.Text = "Vozovi otvori";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton2.ForeColor = System.Drawing.Color.White;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(86, 24);
            this.toolStripButton2.Text = "Dolazeći";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton4.ForeColor = System.Drawing.Color.White;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(87, 24);
            this.toolStripButton4.Text = "Odlazeći";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.toolStripButton5.ForeColor = System.Drawing.Color.White;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(103, 24);
            this.toolStripButton5.Text = "Kopiraj voz";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSifra.Location = new System.Drawing.Point(554, 4);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(79, 22);
            this.txtSifra.TabIndex = 131;
            this.txtSifra.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(485, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 132;
            this.label1.Text = "Šifra:";
            this.label1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1048, 419);
            this.dataGridView1.TabIndex = 130;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.mainHeder);
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.txtSifra);
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1049, 33);
            this.panelHeader.TabIndex = 461;
            // 
            // mainHeder
            // 
            this.mainHeder.BackColor = System.Drawing.Color.Transparent;
            this.mainHeder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mainHeder.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainHeder.Location = new System.Drawing.Point(973, 0);
            this.mainHeder.Name = "mainHeder";
            this.mainHeder.Size = new System.Drawing.Size(74, 31);
            this.mainHeder.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button26);
            this.panel2.Controls.Add(this.button25);
            this.panel2.Controls.Add(this.button24);
            this.panel2.Controls.Add(this.button23);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(731, 31);
            this.panel2.TabIndex = 6;
            // 
            // button26
            // 
            this.button26.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button26.Dock = System.Windows.Forms.DockStyle.Left;
            this.button26.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button26.FlatAppearance.BorderSize = 0;
            this.button26.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button26.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button26.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button26.Location = new System.Drawing.Point(266, 0);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(108, 31);
            this.button26.TabIndex = 19;
            this.button26.Text = "Kopiraj";
            this.button26.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button26.UseVisualStyleBackColor = true;
            this.button26.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // button25
            // 
            this.button25.AutoSize = true;
            this.button25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button25.Dock = System.Windows.Forms.DockStyle.Left;
            this.button25.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button25.FlatAppearance.BorderSize = 0;
            this.button25.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button25.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button25.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button25.Location = new System.Drawing.Point(168, 0);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(98, 31);
            this.button25.TabIndex = 18;
            this.button25.Text = "Odlazeći";
            this.button25.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.toolStripButton4_Click);
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
            this.button24.Location = new System.Drawing.Point(85, 0);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(83, 31);
            this.button24.TabIndex = 17;
            this.button24.Text = "Dolazeći";
            this.button24.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.toolStripButton2_Click_1);
            // 
            // button23
            // 
            this.button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button23.Dock = System.Windows.Forms.DockStyle.Left;
            this.button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button23.FlatAppearance.BorderSize = 0;
            this.button23.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button23.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(85, 31);
            this.button23.TabIndex = 16;
            this.button23.Text = "Otvori";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // frmPregledVozova
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1049, 482);
            this.Controls.Add(this.meniHeader);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelHeader);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPregledVozova";
            this.Text = "Pregled vozova";
            this.Load += new System.EventHandler(this.PrijemVozomPregled_Load);
            this.meniHeader.ResumeLayout(false);
            this.meniHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip meniHeader;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel mainHeder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button23;
    }
}