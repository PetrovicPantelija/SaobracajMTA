namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniLog
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPoDatumu = new System.Windows.Forms.Button();
            this.btnZadnjih1000 = new System.Windows.Forms.Button();
            this.btnZaZapis = new System.Windows.Forms.Button();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblOd = new System.Windows.Forms.Label();
            this.dtpOd = new System.Windows.Forms.DateTimePicker();
            this.lblDo = new System.Windows.Forms.Label();
            this.dtpDo = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.panelHeader.Size = new System.Drawing.Size(1100, 33);
            this.panelHeader.TabIndex = 461;
            //
            // panel2
            //
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnPoDatumu);
            this.panel2.Controls.Add(this.btnZadnjih1000);
            this.panel2.Controls.Add(this.btnZaZapis);
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1090, 31);
            this.panel2.TabIndex = 6;
            //
            // btnPoDatumu
            //
            this.btnPoDatumu.AutoSize = true;
            this.btnPoDatumu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPoDatumu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPoDatumu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnPoDatumu.FlatAppearance.BorderSize = 0;
            this.btnPoDatumu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPoDatumu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPoDatumu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPoDatumu.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnPoDatumu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnPoDatumu.Location = new System.Drawing.Point(400, 0);
            this.btnPoDatumu.Name = "btnPoDatumu";
            this.btnPoDatumu.Size = new System.Drawing.Size(170, 31);
            this.btnPoDatumu.TabIndex = 3;
            this.btnPoDatumu.Text = "Prikaži po datumu";
            this.btnPoDatumu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPoDatumu.UseVisualStyleBackColor = true;
            this.btnPoDatumu.Click += new System.EventHandler(this.btnPoDatumu_Click);
            //
            // btnZadnjih1000
            //
            this.btnZadnjih1000.AutoSize = true;
            this.btnZadnjih1000.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZadnjih1000.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnZadnjih1000.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnZadnjih1000.FlatAppearance.BorderSize = 0;
            this.btnZadnjih1000.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnZadnjih1000.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnZadnjih1000.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZadnjih1000.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnZadnjih1000.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnZadnjih1000.Location = new System.Drawing.Point(200, 0);
            this.btnZadnjih1000.Name = "btnZadnjih1000";
            this.btnZadnjih1000.Size = new System.Drawing.Size(200, 31);
            this.btnZadnjih1000.TabIndex = 2;
            this.btnZadnjih1000.Text = "Prikaži zadnjih 1000";
            this.btnZadnjih1000.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZadnjih1000.UseVisualStyleBackColor = true;
            this.btnZadnjih1000.Click += new System.EventHandler(this.btnZadnjih1000_Click);
            //
            // btnZaZapis
            //
            this.btnZaZapis.AutoSize = true;
            this.btnZaZapis.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZaZapis.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnZaZapis.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnZaZapis.FlatAppearance.BorderSize = 0;
            this.btnZaZapis.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnZaZapis.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnZaZapis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZaZapis.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnZaZapis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnZaZapis.Location = new System.Drawing.Point(0, 0);
            this.btnZaZapis.Name = "btnZaZapis";
            this.btnZaZapis.Size = new System.Drawing.Size(200, 31);
            this.btnZaZapis.TabIndex = 1;
            this.btnZaZapis.Text = "Prikaži za izabrani zapis";
            this.btnZaZapis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZaZapis.UseVisualStyleBackColor = true;
            this.btnZaZapis.Click += new System.EventHandler(this.btnZaZapis_Click);
            //
            // pnlFilter
            //
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.pnlFilter.Controls.Add(this.lblOd);
            this.pnlFilter.Controls.Add(this.dtpOd);
            this.pnlFilter.Controls.Add(this.lblDo);
            this.pnlFilter.Controls.Add(this.dtpDo);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 33);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1100, 36);
            this.pnlFilter.TabIndex = 1;
            //
            // lblOd
            //
            this.lblOd.AutoSize = true;
            this.lblOd.Location = new System.Drawing.Point(10, 12);
            this.lblOd.Name = "lblOd";
            this.lblOd.Size = new System.Drawing.Size(21, 13);
            this.lblOd.TabIndex = 0;
            this.lblOd.Text = "Od";
            //
            // dtpOd
            //
            this.dtpOd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpOd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOd.Location = new System.Drawing.Point(40, 8);
            this.dtpOd.Name = "dtpOd";
            this.dtpOd.Size = new System.Drawing.Size(170, 20);
            this.dtpOd.TabIndex = 1;
            //
            // lblDo
            //
            this.lblDo.AutoSize = true;
            this.lblDo.Location = new System.Drawing.Point(230, 12);
            this.lblDo.Name = "lblDo";
            this.lblDo.Size = new System.Drawing.Size(21, 13);
            this.lblDo.TabIndex = 2;
            this.lblDo.Text = "Do";
            //
            // dtpDo
            //
            this.dtpDo.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.dtpDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDo.Location = new System.Drawing.Point(260, 8);
            this.dtpDo.Name = "dtpDo";
            this.dtpDo.Size = new System.Drawing.Size(170, 20);
            this.dtpDo.TabIndex = 3;
            //
            // lblStatus
            //
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 577);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(6, 4, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(1100, 23);
            this.lblStatus.TabIndex = 3;
            //
            // dataGridView1
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 69);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1100, 508);
            this.dataGridView1.TabIndex = 2;
            //
            // frmTerminalPrivremeniLog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmTerminalPrivremeniLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal privremeni - log";
            this.Load += new System.EventHandler(this.frmTerminalPrivremeniLog_Load);
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPoDatumu;
        private System.Windows.Forms.Button btnZadnjih1000;
        private System.Windows.Forms.Button btnZaZapis;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblOd;
        private System.Windows.Forms.DateTimePicker dtpOd;
        private System.Windows.Forms.Label lblDo;
        private System.Windows.Forms.DateTimePicker dtpDo;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
