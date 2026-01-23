namespace Saobracaj.TerminalMap
{
    partial class TerminalMapFRM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalMapFRM));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pozadinaONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditMode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSaveRegions = new System.Windows.Forms.ToolStripMenuItem();
            this.btnResetViews = new System.Windows.Forms.ToolStripMenuItem();
            this.lblHangar = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbAktivan = new System.Windows.Forms.CheckBox();
            this.sfButton5 = new Syncfusion.WinForms.Controls.SfButton();
            this.cbPrazno = new System.Windows.Forms.CheckBox();
            this.sfButton4 = new Syncfusion.WinForms.Controls.SfButton();
            this.cbPrazniSe = new System.Windows.Forms.CheckBox();
            this.sfButton3 = new Syncfusion.WinForms.Controls.SfButton();
            this.btnPrijemIOtpremaKamiona3 = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButton2 = new Syncfusion.WinForms.Controls.SfButton();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.map1 = new Saobracaj.TerminalMap.TerminalMapControl();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pozadinaONToolStripMenuItem,
            this.btnEditMode,
            this.btnSaveRegions,
            this.btnResetViews,
            this.lblHangar});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1055, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pozadinaONToolStripMenuItem
            // 
            this.pozadinaONToolStripMenuItem.ForeColor = System.Drawing.Color.Lime;
            this.pozadinaONToolStripMenuItem.Name = "pozadinaONToolStripMenuItem";
            this.pozadinaONToolStripMenuItem.Size = new System.Drawing.Size(106, 24);
            this.pozadinaONToolStripMenuItem.Text = "Pozadina:ON";
            this.pozadinaONToolStripMenuItem.Click += new System.EventHandler(this.pozadinaONToolStripMenuItem_Click);
            // 
            // btnEditMode
            // 
            this.btnEditMode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditMode.ForeColor = System.Drawing.Color.Red;
            this.btnEditMode.Name = "btnEditMode";
            this.btnEditMode.Size = new System.Drawing.Size(79, 24);
            this.btnEditMode.Text = "Edit: OFF";
            // 
            // btnSaveRegions
            // 
            this.btnSaveRegions.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRegions.ForeColor = System.Drawing.Color.White;
            this.btnSaveRegions.Name = "btnSaveRegions";
            this.btnSaveRegions.Size = new System.Drawing.Size(105, 24);
            this.btnSaveRegions.Text = "Save regions";
            // 
            // btnResetViews
            // 
            this.btnResetViews.ForeColor = System.Drawing.Color.White;
            this.btnResetViews.Name = "btnResetViews";
            this.btnResetViews.Size = new System.Drawing.Size(91, 24);
            this.btnResetViews.Text = "Reset view";
            // 
            // lblHangar
            // 
            this.lblHangar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblHangar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHangar.ForeColor = System.Drawing.Color.White;
            this.lblHangar.Name = "lblHangar";
            this.lblHangar.Size = new System.Drawing.Size(87, 24);
            this.lblHangar.Text = "Skladište:";
            this.lblHangar.Click += new System.EventHandler(this.lblHangar_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dgvStock);
            this.panel1.Location = new System.Drawing.Point(12, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 465);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbAktivan);
            this.panel2.Controls.Add(this.sfButton5);
            this.panel2.Controls.Add(this.cbPrazno);
            this.panel2.Controls.Add(this.sfButton4);
            this.panel2.Controls.Add(this.cbPrazniSe);
            this.panel2.Controls.Add(this.sfButton3);
            this.panel2.Controls.Add(this.btnPrijemIOtpremaKamiona3);
            this.panel2.Controls.Add(this.sfButton2);
            this.panel2.Location = new System.Drawing.Point(845, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(179, 455);
            this.panel2.TabIndex = 521;
            // 
            // cbAktivan
            // 
            this.cbAktivan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAktivan.AutoSize = true;
            this.cbAktivan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAktivan.ForeColor = System.Drawing.Color.Black;
            this.cbAktivan.Location = new System.Drawing.Point(11, 14);
            this.cbAktivan.Name = "cbAktivan";
            this.cbAktivan.Size = new System.Drawing.Size(70, 20);
            this.cbAktivan.TabIndex = 1;
            this.cbAktivan.Text = "Aktivan";
            this.cbAktivan.UseVisualStyleBackColor = true;
            this.cbAktivan.CheckStateChanged += new System.EventHandler(this.cbAktivan_CheckStateChanged);
            // 
            // sfButton5
            // 
            this.sfButton5.AccessibleName = "Button";
            this.sfButton5.AllowWrapText = true;
            this.sfButton5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sfButton5.ForeColor = System.Drawing.Color.White;
            this.sfButton5.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.sfButton5.ImageSize = new System.Drawing.Size(70, 80);
            this.sfButton5.Location = new System.Drawing.Point(3, 385);
            this.sfButton5.Name = "sfButton5";
            this.sfButton5.Size = new System.Drawing.Size(170, 42);
            this.sfButton5.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton5.Style.ForeColor = System.Drawing.Color.White;
            this.sfButton5.TabIndex = 520;
            this.sfButton5.Text = "Dodatne usluge";
            this.sfButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sfButton5.UseVisualStyleBackColor = false;
            this.sfButton5.Click += new System.EventHandler(this.sfButton5_Click);
            // 
            // cbPrazno
            // 
            this.cbPrazno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPrazno.AutoSize = true;
            this.cbPrazno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrazno.ForeColor = System.Drawing.Color.Black;
            this.cbPrazno.Location = new System.Drawing.Point(11, 40);
            this.cbPrazno.Name = "cbPrazno";
            this.cbPrazno.Size = new System.Drawing.Size(68, 20);
            this.cbPrazno.TabIndex = 1;
            this.cbPrazno.Text = "Prazno";
            this.cbPrazno.UseVisualStyleBackColor = true;
            this.cbPrazno.CheckStateChanged += new System.EventHandler(this.cbPrazno_CheckStateChanged);
            // 
            // sfButton4
            // 
            this.sfButton4.AccessibleName = "Button";
            this.sfButton4.AllowWrapText = true;
            this.sfButton4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sfButton4.ForeColor = System.Drawing.Color.White;
            this.sfButton4.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.sfButton4.ImageSize = new System.Drawing.Size(70, 80);
            this.sfButton4.Location = new System.Drawing.Point(2, 337);
            this.sfButton4.Name = "sfButton4";
            this.sfButton4.Size = new System.Drawing.Size(171, 42);
            this.sfButton4.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton4.Style.ForeColor = System.Drawing.Color.White;
            this.sfButton4.TabIndex = 519;
            this.sfButton4.Text = "Interni prenos";
            this.sfButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sfButton4.UseVisualStyleBackColor = false;
            this.sfButton4.Click += new System.EventHandler(this.sfButton4_Click);
            // 
            // cbPrazniSe
            // 
            // sfButton3
            // 
            this.sfButton3.AccessibleName = "Button";
            this.sfButton3.AllowWrapText = true;
            this.sfButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sfButton3.ForeColor = System.Drawing.Color.White;
            this.sfButton3.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.sfButton3.ImageSize = new System.Drawing.Size(70, 80);
            this.sfButton3.Location = new System.Drawing.Point(3, 289);
            this.sfButton3.Name = "sfButton3";
            this.sfButton3.Size = new System.Drawing.Size(169, 42);
            this.sfButton3.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton3.Style.ForeColor = System.Drawing.Color.White;
            this.sfButton3.TabIndex = 518;
            this.sfButton3.Text = "Terminal central";
            this.sfButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sfButton3.UseVisualStyleBackColor = false;
            this.sfButton3.Click += new System.EventHandler(this.sfButton3_Click);
            // 
            // sfButton2
            // 
            this.sfButton2.AccessibleName = "Button";
            this.sfButton2.AllowWrapText = true;
            this.sfButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sfButton2.ForeColor = System.Drawing.Color.White;
            this.sfButton2.ImageMargin = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.sfButton2.ImageSize = new System.Drawing.Size(70, 80);
            this.sfButton2.Location = new System.Drawing.Point(3, 241);
            this.sfButton2.Name = "sfButton2";
            this.sfButton2.Size = new System.Drawing.Size(169, 42);
            this.sfButton2.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(61)))), ((int)(((byte)(85)))));
            this.sfButton2.Style.ForeColor = System.Drawing.Color.White;
            this.sfButton2.TabIndex = 517;
            this.sfButton2.Text = "Popis cnt detaljan";
            this.sfButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sfButton2.UseVisualStyleBackColor = false;
            this.sfButton2.Click += new System.EventHandler(this.sfButton2_Click);
            // 
            // dgvStock
            // 
            this.dgvStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Location = new System.Drawing.Point(-2, -2);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.Size = new System.Drawing.Size(841, 460);
            this.dgvStock.TabIndex = 0;
            // 
            // map1
            // 
            this.map1.CanvasBackColor = System.Drawing.Color.White;
            this.map1.CurrentDrawColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map1.EditMode = false;
            this.map1.ImageOffSet = ((System.Drawing.PointF)(resources.GetObject("map1.ImageOffSet")));
            this.map1.Location = new System.Drawing.Point(0, 28);
            this.map1.Name = "map1";
            this.map1.ShowBaseImage = true;
            this.map1.Size = new System.Drawing.Size(1055, 524);
            this.map1.StretchMode = Saobracaj.TerminalMap.MapStretchMode.Fill;
            this.map1.TabIndex = 1;
            this.map1.Text = "terminalMapControl1";
            this.map1.Click += new System.EventHandler(this.map1_Click);
            // 
            // TerminalMapFRM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.map1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "TerminalMapFRM";
            this.Text = "TerminalMapFRM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TerminalMapFRM_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnEditMode;
        private System.Windows.Forms.ToolStripMenuItem btnSaveRegions;
        private System.Windows.Forms.ToolStripMenuItem btnResetViews;
        private System.Windows.Forms.ToolStripMenuItem lblHangar;
        private TerminalMapControl map1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.CheckBox cbPrazniSe;
        private System.Windows.Forms.CheckBox cbPrazno;
        private System.Windows.Forms.CheckBox cbAktivan;
        private System.Windows.Forms.ToolStripMenuItem pozadinaONToolStripMenuItem;
        private Syncfusion.WinForms.Controls.SfButton sfButton5;
        private Syncfusion.WinForms.Controls.SfButton sfButton4;
        private Syncfusion.WinForms.Controls.SfButton sfButton3;
        private Syncfusion.WinForms.Controls.SfButton sfButton2;
        private Syncfusion.WinForms.Controls.SfButton btnPrijemIOtpremaKamiona3;
        private System.Windows.Forms.Panel panel2;
    }
}