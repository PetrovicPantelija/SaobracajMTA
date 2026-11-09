namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniSlike
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOtvoriFolder = new System.Windows.Forms.Button();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnDodajSlike = new System.Windows.Forms.Button();
            this.pnlDrop = new System.Windows.Forms.Panel();
            this.lblDrop = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ofdDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // panelHeader
            //
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 33);
            this.panelHeader.TabIndex = 461;
            //
            // panel2
            //
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnOtvoriFolder);
            this.panel2.Controls.Add(this.btnOsvezi);
            this.panel2.Controls.Add(this.btnDodajSlike);
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(990, 31);
            this.panel2.TabIndex = 6;
            //
            // btnOtvoriFolder
            //
            this.btnOtvoriFolder.AutoSize = true;
            this.btnOtvoriFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOtvoriFolder.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOtvoriFolder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnOtvoriFolder.FlatAppearance.BorderSize = 0;
            this.btnOtvoriFolder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOtvoriFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOtvoriFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtvoriFolder.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnOtvoriFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnOtvoriFolder.Location = new System.Drawing.Point(240, 0);
            this.btnOtvoriFolder.Name = "btnOtvoriFolder";
            this.btnOtvoriFolder.Size = new System.Drawing.Size(120, 31);
            this.btnOtvoriFolder.TabIndex = 3;
            this.btnOtvoriFolder.Text = "Otvori folder";
            this.btnOtvoriFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOtvoriFolder.UseVisualStyleBackColor = true;
            this.btnOtvoriFolder.Click += new System.EventHandler(this.btnOtvoriFolder_Click);
            //
            // btnOsvezi
            //
            this.btnOsvezi.AutoSize = true;
            this.btnOsvezi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOsvezi.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOsvezi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnOsvezi.FlatAppearance.BorderSize = 0;
            this.btnOsvezi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOsvezi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOsvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOsvezi.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnOsvezi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnOsvezi.Location = new System.Drawing.Point(120, 0);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(120, 31);
            this.btnOsvezi.TabIndex = 2;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            //
            // btnDodajSlike
            //
            this.btnDodajSlike.AutoSize = true;
            this.btnDodajSlike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajSlike.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDodajSlike.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnDodajSlike.FlatAppearance.BorderSize = 0;
            this.btnDodajSlike.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDodajSlike.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnDodajSlike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajSlike.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnDodajSlike.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnDodajSlike.Location = new System.Drawing.Point(0, 0);
            this.btnDodajSlike.Name = "btnDodajSlike";
            this.btnDodajSlike.Size = new System.Drawing.Size(120, 31);
            this.btnDodajSlike.TabIndex = 1;
            this.btnDodajSlike.Text = "Dodaj slike...";
            this.btnDodajSlike.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDodajSlike.UseVisualStyleBackColor = true;
            this.btnDodajSlike.Click += new System.EventHandler(this.btnDodajSlike_Click);
            //
            // pnlDrop
            //
            this.pnlDrop.AllowDrop = true;
            this.pnlDrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlDrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDrop.Controls.Add(this.lblDrop);
            this.pnlDrop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDrop.Location = new System.Drawing.Point(0, 33);
            this.pnlDrop.Name = "pnlDrop";
            this.pnlDrop.Size = new System.Drawing.Size(1000, 70);
            this.pnlDrop.TabIndex = 1;
            this.pnlDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.Slike_DragDrop);
            this.pnlDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.Slike_DragEnter);
            //
            // lblDrop
            //
            this.lblDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDrop.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrop.Location = new System.Drawing.Point(0, 0);
            this.lblDrop.Name = "lblDrop";
            this.lblDrop.Size = new System.Drawing.Size(998, 68);
            this.lblDrop.TabIndex = 0;
            this.lblDrop.Text = "Prevucite slike ovde (drag && drop)";
            this.lblDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.Slike_DragDrop);
            this.lblDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.Slike_DragEnter);
            //
            // lblStatus
            //
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 577);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(6, 4, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(1000, 23);
            this.lblStatus.TabIndex = 3;
            //
            // splitContainer1
            //
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 103);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 474);
            this.splitContainer1.SplitterDistance = 520;
            this.splitContainer1.TabIndex = 2;
            //
            // listView1
            //
            this.listView1.AllowDrop = true;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(520, 474);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.Slike_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Slike_DragEnter);
            //
            // imageList1
            //
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(120, 120);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            //
            // pictureBox1
            //
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(476, 474);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            //
            // ofdDialog
            //
            this.ofdDialog.Filter = "Slike (*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tif;*.tiff";
            this.ofdDialog.Multiselect = true;
            this.ofdDialog.Title = "Izaberite slike";
            //
            // frmTerminalPrivremeniSlike
            //
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlDrop);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmTerminalPrivremeniSlike";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal privremeni - slike";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Slike_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Slike_DragEnter);
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlDrop.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOtvoriFolder;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button btnDodajSlike;
        private System.Windows.Forms.Panel pnlDrop;
        private System.Windows.Forms.Label lblDrop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog ofdDialog;
    }
}
