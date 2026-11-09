namespace Saobracaj.Skladista_main
{
    partial class frmMestoIsporuke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMestoIsporuke));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.txtKorisnik = new System.Windows.Forms.TextBox();
            this.btnSnimi = new System.Windows.Forms.Button();
            this.panelPrijem1 = new System.Windows.Forms.Panel();
            this.cboKontaktOsobaPrijem = new System.Windows.Forms.ComboBox();
            this.cboAdresaPrijem = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.cboMestoIstovaraPrijem = new System.Windows.Forms.ComboBox();
            this.meniHeader = new System.Windows.Forms.ToolStrip();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelPrijem1.SuspendLayout();
            this.meniHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.txtSifra);
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(878, 39);
            this.panelHeader.TabIndex = 558;
            // 
            // txtSifra
            // 
            this.txtSifra.Location = new System.Drawing.Point(1501, 6);
            this.txtSifra.Margin = new System.Windows.Forms.Padding(4);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(132, 22);
            this.txtSifra.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label20);
            this.panel2.Controls.Add(this.txtKorisnik);
            this.panel2.Controls.Add(this.btnSnimi);
            this.panel2.Location = new System.Drawing.Point(15, -1);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 39);
            this.panel2.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(1104, 16);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 16);
            this.label20.TabIndex = 560;
            this.label20.Text = "KORISNIK";
            // 
            // txtKorisnik
            // 
            this.txtKorisnik.Location = new System.Drawing.Point(1190, 12);
            this.txtKorisnik.Margin = new System.Windows.Forms.Padding(4);
            this.txtKorisnik.Name = "txtKorisnik";
            this.txtKorisnik.Size = new System.Drawing.Size(247, 22);
            this.txtKorisnik.TabIndex = 561;
            // 
            // btnSnimi
            // 
            this.btnSnimi.AutoSize = true;
            this.btnSnimi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSnimi.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSnimi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnSnimi.FlatAppearance.BorderSize = 0;
            this.btnSnimi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSnimi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSnimi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSnimi.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnSnimi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnSnimi.Location = new System.Drawing.Point(0, 0);
            this.btnSnimi.Margin = new System.Windows.Forms.Padding(4);
            this.btnSnimi.Name = "btnSnimi";
            this.btnSnimi.Size = new System.Drawing.Size(93, 39);
            this.btnSnimi.TabIndex = 17;
            this.btnSnimi.Text = "Snimi";
            this.btnSnimi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSnimi.UseVisualStyleBackColor = true;
            this.btnSnimi.Click += new System.EventHandler(this.btnSnimi_Click);
            // 
            // panelPrijem1
            // 
            this.panelPrijem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelPrijem1.Controls.Add(this.meniHeader);
            this.panelPrijem1.Controls.Add(this.cboKontaktOsobaPrijem);
            this.panelPrijem1.Controls.Add(this.cboAdresaPrijem);
            this.panelPrijem1.Controls.Add(this.label22);
            this.panelPrijem1.Controls.Add(this.label23);
            this.panelPrijem1.Controls.Add(this.label30);
            this.panelPrijem1.Controls.Add(this.cboMestoIstovaraPrijem);
            this.panelPrijem1.Location = new System.Drawing.Point(5, 46);
            this.panelPrijem1.Margin = new System.Windows.Forms.Padding(4);
            this.panelPrijem1.Name = "panelPrijem1";
            this.panelPrijem1.Size = new System.Drawing.Size(868, 200);
            this.panelPrijem1.TabIndex = 557;
            // 
            // cboKontaktOsobaPrijem
            // 
            this.cboKontaktOsobaPrijem.FormattingEnabled = true;
            this.cboKontaktOsobaPrijem.Location = new System.Drawing.Point(581, 67);
            this.cboKontaktOsobaPrijem.Margin = new System.Windows.Forms.Padding(4);
            this.cboKontaktOsobaPrijem.Name = "cboKontaktOsobaPrijem";
            this.cboKontaktOsobaPrijem.Size = new System.Drawing.Size(200, 24);
            this.cboKontaktOsobaPrijem.TabIndex = 466;
            // 
            // cboAdresaPrijem
            // 
            this.cboAdresaPrijem.FormattingEnabled = true;
            this.cboAdresaPrijem.Location = new System.Drawing.Point(329, 67);
            this.cboAdresaPrijem.Margin = new System.Windows.Forms.Padding(4);
            this.cboAdresaPrijem.Name = "cboAdresaPrijem";
            this.cboAdresaPrijem.Size = new System.Drawing.Size(200, 24);
            this.cboAdresaPrijem.TabIndex = 465;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(578, 47);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(120, 16);
            this.label22.TabIndex = 461;
            this.label22.Text = "KONTAKT OSOBA";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(325, 46);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 16);
            this.label23.TabIndex = 462;
            this.label23.Text = "ADRESA";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label30.Location = new System.Drawing.Point(63, 47);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(185, 16);
            this.label30.TabIndex = 463;
            this.label30.Text = "MESTO ISTOVARA CERADE";
            // 
            // cboMestoIstovaraPrijem
            // 
            this.cboMestoIstovaraPrijem.FormattingEnabled = true;
            this.cboMestoIstovaraPrijem.Location = new System.Drawing.Point(63, 67);
            this.cboMestoIstovaraPrijem.Margin = new System.Windows.Forms.Padding(4);
            this.cboMestoIstovaraPrijem.Name = "cboMestoIstovaraPrijem";
            this.cboMestoIstovaraPrijem.Size = new System.Drawing.Size(197, 24);
            this.cboMestoIstovaraPrijem.TabIndex = 464;
            this.cboMestoIstovaraPrijem.Leave += new System.EventHandler(this.cboMestoIstovaraPrijem_Leave);
            // 
            // meniHeader
            // 
            this.meniHeader.BackColor = System.Drawing.Color.DodgerBlue;
            this.meniHeader.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.meniHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSave,
            this.toolStripSeparator1,
            this.toolStripButton2});
            this.meniHeader.Location = new System.Drawing.Point(0, 0);
            this.meniHeader.Name = "meniHeader";
            this.meniHeader.Size = new System.Drawing.Size(868, 27);
            this.meniHeader.TabIndex = 600;
            this.meniHeader.Text = "toolStrip1";
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(29, 24);
            this.tsSave.Text = "tsSave";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // frmMestoIsporuke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 252);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelPrijem1);
            this.Name = "frmMestoIsporuke";
            this.Text = "frmMestoIsporuke";
            this.Load += new System.EventHandler(this.frmMestoIsporuke_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelPrijem1.ResumeLayout(false);
            this.panelPrijem1.PerformLayout();
            this.meniHeader.ResumeLayout(false);
            this.meniHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtKorisnik;
        private System.Windows.Forms.Button btnSnimi;
        private System.Windows.Forms.Panel panelPrijem1;
        private System.Windows.Forms.ComboBox cboKontaktOsobaPrijem;
        private System.Windows.Forms.ComboBox cboAdresaPrijem;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cboMestoIstovaraPrijem;
        private System.Windows.Forms.ToolStrip meniHeader;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}