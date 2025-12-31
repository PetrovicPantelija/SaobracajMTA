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
            this.cbPrazniSe = new System.Windows.Forms.CheckBox();
            this.cbPrazno = new System.Windows.Forms.CheckBox();
            this.cbAktivan = new System.Windows.Forms.CheckBox();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.map1 = new Saobracaj.TerminalMap.TerminalMapControl();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.RoyalBlue;
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
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.cbPrazniSe);
            this.panel1.Controls.Add(this.cbPrazno);
            this.panel1.Controls.Add(this.cbAktivan);
            this.panel1.Controls.Add(this.dgvStock);
            this.panel1.Location = new System.Drawing.Point(394, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(639, 239);
            this.panel1.TabIndex = 2;
            // 
            // cbPrazniSe
            // 
            this.cbPrazniSe.AutoSize = true;
            this.cbPrazniSe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrazniSe.ForeColor = System.Drawing.Color.White;
            this.cbPrazniSe.Location = new System.Drawing.Point(542, 65);
            this.cbPrazniSe.Name = "cbPrazniSe";
            this.cbPrazniSe.Size = new System.Drawing.Size(81, 20);
            this.cbPrazniSe.TabIndex = 1;
            this.cbPrazniSe.Text = "Prazni se";
            this.cbPrazniSe.UseVisualStyleBackColor = true;
            this.cbPrazniSe.CheckStateChanged += new System.EventHandler(this.cbPrazniSe_CheckStateChanged);
            // 
            // cbPrazno
            // 
            this.cbPrazno.AutoSize = true;
            this.cbPrazno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrazno.ForeColor = System.Drawing.Color.White;
            this.cbPrazno.Location = new System.Drawing.Point(542, 39);
            this.cbPrazno.Name = "cbPrazno";
            this.cbPrazno.Size = new System.Drawing.Size(68, 20);
            this.cbPrazno.TabIndex = 1;
            this.cbPrazno.Text = "Prazno";
            this.cbPrazno.UseVisualStyleBackColor = true;
            this.cbPrazno.CheckStateChanged += new System.EventHandler(this.cbPrazno_CheckStateChanged);
            // 
            // cbAktivan
            // 
            this.cbAktivan.AutoSize = true;
            this.cbAktivan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAktivan.ForeColor = System.Drawing.Color.White;
            this.cbAktivan.Location = new System.Drawing.Point(542, 13);
            this.cbAktivan.Name = "cbAktivan";
            this.cbAktivan.Size = new System.Drawing.Size(70, 20);
            this.cbAktivan.TabIndex = 1;
            this.cbAktivan.Text = "Aktivan";
            this.cbAktivan.UseVisualStyleBackColor = true;
            this.cbAktivan.CheckStateChanged += new System.EventHandler(this.cbAktivan_CheckStateChanged);
            // 
            // dgvStock
            // 
            this.dgvStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Location = new System.Drawing.Point(3, 3);
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.Size = new System.Drawing.Size(533, 229);
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
            this.panel1.PerformLayout();
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
    }
}