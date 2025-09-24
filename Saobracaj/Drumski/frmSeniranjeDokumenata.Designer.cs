namespace Saobracaj.Drumski
{
    partial class frmSeniranjeDokumenata
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.lblScanners = new System.Windows.Forms.Label();
            this.cmbScanners = new System.Windows.Forms.ComboBox();
            this.btnRefreshScanners = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.lbPages = new System.Windows.Forms.ListBox();
            this.btnEditImage = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSaveInvoice = new System.Windows.Forms.Button();
            this.btnSaveTransport = new System.Windows.Forms.Button();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.label1);
            this.leftPanel.Controls.Add(this.lblScanners);
            this.leftPanel.Controls.Add(this.cmbScanners);
            this.leftPanel.Controls.Add(this.btnRefreshScanners);
            this.leftPanel.Controls.Add(this.btnScan);
            this.leftPanel.Controls.Add(this.lbPages);
            this.leftPanel.Controls.Add(this.btnEditImage);
            this.leftPanel.Controls.Add(this.btnDelete);
            this.leftPanel.Controls.Add(this.btnSaveInvoice);
            this.leftPanel.Controls.Add(this.btnSaveTransport);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Padding = new System.Windows.Forms.Padding(8);
            this.leftPanel.Size = new System.Drawing.Size(300, 700);
            this.leftPanel.TabIndex = 1;
            // 
            // lblScanners
            // 
            this.lblScanners.AutoSize = true;
            this.lblScanners.Location = new System.Drawing.Point(10, 8);
            this.lblScanners.Name = "lblScanners";
            this.lblScanners.Size = new System.Drawing.Size(114, 16);
            this.lblScanners.TabIndex = 0;
            this.lblScanners.Text = "Odaberite skener:";
            // 
            // cmbScanners
            // 
            this.cmbScanners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanners.Location = new System.Drawing.Point(11, 30);
            this.cmbScanners.Name = "cmbScanners";
            this.cmbScanners.Size = new System.Drawing.Size(260, 24);
            this.cmbScanners.TabIndex = 1;
            // 
            // btnRefreshScanners
            // 
            this.btnRefreshScanners.Location = new System.Drawing.Point(11, 60);
            this.btnRefreshScanners.Name = "btnRefreshScanners";
            this.btnRefreshScanners.Size = new System.Drawing.Size(120, 29);
            this.btnRefreshScanners.TabIndex = 2;
            this.btnRefreshScanners.Text = "Osveži listu";
            this.btnRefreshScanners.Click += new System.EventHandler(this.BtnRefreshScanners_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(11, 96);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(260, 29);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "Započni skeniranje";
            this.btnScan.Click += new System.EventHandler(this.BtnScan_Click);
            // 
            // lbPages
            // 
            this.lbPages.ItemHeight = 16;
            this.lbPages.Location = new System.Drawing.Point(12, 177);
            this.lbPages.Name = "lbPages";
            this.lbPages.Size = new System.Drawing.Size(260, 324);
            this.lbPages.TabIndex = 4;
            this.lbPages.SelectedIndexChanged += new System.EventHandler(this.LbPages_SelectedIndexChanged);
            // 
            // btnEditImage
            // 
            this.btnEditImage.Location = new System.Drawing.Point(11, 510);
            this.btnEditImage.Name = "btnEditImage";
            this.btnEditImage.Size = new System.Drawing.Size(120, 29);
            this.btnEditImage.TabIndex = 5;
            this.btnEditImage.Text = "Započni izmenu";
            this.btnEditImage.Click += new System.EventHandler(this.btnEditImage_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(151, 510);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 29);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Obriši stranu";
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnSaveInvoice
            // 
            this.btnSaveInvoice.Location = new System.Drawing.Point(11, 623);
            this.btnSaveInvoice.Name = "btnSaveInvoice";
            this.btnSaveInvoice.Size = new System.Drawing.Size(260, 29);
            this.btnSaveInvoice.TabIndex = 7;
            this.btnSaveInvoice.Text = "Snimi kao fakturu";
            this.btnSaveInvoice.Click += new System.EventHandler(this.BtnSaveFaktura_Click);
            // 
            // btnSaveTransport
            // 
            this.btnSaveTransport.Location = new System.Drawing.Point(12, 578);
            this.btnSaveTransport.Name = "btnSaveTransport";
            this.btnSaveTransport.Size = new System.Drawing.Size(260, 29);
            this.btnSaveTransport.TabIndex = 8;
            this.btnSaveTransport.Text = "Snimi kao prevoznički dokument";
            this.btnSaveTransport.Click += new System.EventHandler(this.BtnSaveTransport_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.pbPreview);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(300, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Padding = new System.Windows.Forms.Padding(8);
            this.rightPanel.Size = new System.Drawing.Size(900, 700);
            this.rightPanel.TabIndex = 0;
            // 
            // pbPreview
            // 
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(8, 8);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(884, 684);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 0;
            this.pbPreview.TabStop = false;
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Strane za tekući pdf:";
            // 
            // frmSeniranjeDokumenata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.rightPanel);
            this.Controls.Add(this.leftPanel);
            this.Name = "frmSeniranjeDokumenata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Skeniranje dokumenata";
            this.Load += new System.EventHandler(this.frmSeniranjeDokumenata_Load);
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.Label lblScanners;
        private System.Windows.Forms.ComboBox cmbScanners;
        private System.Windows.Forms.Button btnRefreshScanners;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox lbPages;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSaveInvoice;
        private System.Windows.Forms.Button btnSaveTransport;
        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.PictureBox pbPreview;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Button btnEditImage;
        private System.Windows.Forms.Label label1;
    }
}