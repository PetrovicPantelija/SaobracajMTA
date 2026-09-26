namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniDokumenta
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
            this.btnOtvoriDokument = new System.Windows.Forms.Button();
            this.btnOsvezi = new System.Windows.Forms.Button();
            this.btnDodajDokumenta = new System.Windows.Forms.Button();
            this.pnlDrop = new System.Windows.Forms.Panel();
            this.lblDrop = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.ofdDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlDrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
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
            this.panel2.Controls.Add(this.btnOtvoriDokument);
            this.panel2.Controls.Add(this.btnOsvezi);
            this.panel2.Controls.Add(this.btnDodajDokumenta);
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
            this.btnOtvoriFolder.Location = new System.Drawing.Point(420, 0);
            this.btnOtvoriFolder.Name = "btnOtvoriFolder";
            this.btnOtvoriFolder.Size = new System.Drawing.Size(120, 31);
            this.btnOtvoriFolder.TabIndex = 4;
            this.btnOtvoriFolder.Text = "Otvori folder";
            this.btnOtvoriFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOtvoriFolder.UseVisualStyleBackColor = true;
            this.btnOtvoriFolder.Click += new System.EventHandler(this.btnOtvoriFolder_Click);
            //
            // btnOtvoriDokument
            //
            this.btnOtvoriDokument.AutoSize = true;
            this.btnOtvoriDokument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOtvoriDokument.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOtvoriDokument.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnOtvoriDokument.FlatAppearance.BorderSize = 0;
            this.btnOtvoriDokument.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOtvoriDokument.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOtvoriDokument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtvoriDokument.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnOtvoriDokument.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnOtvoriDokument.Location = new System.Drawing.Point(240, 0);
            this.btnOtvoriDokument.Name = "btnOtvoriDokument";
            this.btnOtvoriDokument.Size = new System.Drawing.Size(180, 31);
            this.btnOtvoriDokument.TabIndex = 3;
            this.btnOtvoriDokument.Text = "Otvori izabrani dokument";
            this.btnOtvoriDokument.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOtvoriDokument.UseVisualStyleBackColor = true;
            this.btnOtvoriDokument.Click += new System.EventHandler(this.btnOtvoriDokument_Click);
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
            this.btnOsvezi.Location = new System.Drawing.Point(160, 0);
            this.btnOsvezi.Name = "btnOsvezi";
            this.btnOsvezi.Size = new System.Drawing.Size(80, 31);
            this.btnOsvezi.TabIndex = 2;
            this.btnOsvezi.Text = "Osveži";
            this.btnOsvezi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOsvezi.UseVisualStyleBackColor = true;
            this.btnOsvezi.Click += new System.EventHandler(this.btnOsvezi_Click);
            //
            // btnDodajDokumenta
            //
            this.btnDodajDokumenta.AutoSize = true;
            this.btnDodajDokumenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDodajDokumenta.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDodajDokumenta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnDodajDokumenta.FlatAppearance.BorderSize = 0;
            this.btnDodajDokumenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDodajDokumenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnDodajDokumenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDodajDokumenta.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnDodajDokumenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnDodajDokumenta.Location = new System.Drawing.Point(0, 0);
            this.btnDodajDokumenta.Name = "btnDodajDokumenta";
            this.btnDodajDokumenta.Size = new System.Drawing.Size(160, 31);
            this.btnDodajDokumenta.TabIndex = 1;
            this.btnDodajDokumenta.Text = "Dodaj dokumenta...";
            this.btnDodajDokumenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDodajDokumenta.UseVisualStyleBackColor = true;
            this.btnDodajDokumenta.Click += new System.EventHandler(this.btnDodajDokumenta_Click);
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
            this.pnlDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragDrop);
            this.pnlDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragEnter);
            //
            // lblDrop
            //
            this.lblDrop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDrop.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrop.Location = new System.Drawing.Point(0, 0);
            this.lblDrop.Name = "lblDrop";
            this.lblDrop.Size = new System.Drawing.Size(998, 68);
            this.lblDrop.TabIndex = 0;
            this.lblDrop.Text = "Prevucite dokumenta ovde (drag && drop)";
            this.lblDrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDrop.DragDrop += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragDrop);
            this.lblDrop.DragEnter += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragEnter);
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
            // dataGridView2
            //
            this.dataGridView2.AllowDrop = true;
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 103);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(1000, 474);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellDoubleClick);
            this.dataGridView2.DragDrop += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragDrop);
            this.dataGridView2.DragEnter += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragEnter);
            //
            // ofdDialog
            //
            this.ofdDialog.Filter = "Svi fajlovi (*.*)|*.*";
            this.ofdDialog.Multiselect = true;
            this.ofdDialog.Title = "Izaberite dokumenta";
            //
            // frmTerminalPrivremeniDokumenta
            //
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlDrop);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmTerminalPrivremeniDokumenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal privremeni - dokumenta";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Dokumenta_DragEnter);
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlDrop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOtvoriFolder;
        private System.Windows.Forms.Button btnOtvoriDokument;
        private System.Windows.Forms.Button btnOsvezi;
        private System.Windows.Forms.Button btnDodajDokumenta;
        private System.Windows.Forms.Panel pnlDrop;
        private System.Windows.Forms.Label lblDrop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.OpenFileDialog ofdDialog;
    }
}
