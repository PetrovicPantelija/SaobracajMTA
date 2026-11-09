namespace Saobracaj.Skladista_main.Dokumenta
{
    partial class PregledLagera
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblPrva = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.cboPrvi = new System.Windows.Forms.ComboBox();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 64);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1142, 565);
            this.dataGridView1.TabIndex = 1;
            // 
            // lblPrva
            // 
            this.lblPrva.AutoSize = true;
            this.lblPrva.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrva.Location = new System.Drawing.Point(12, 9);
            this.lblPrva.Name = "lblPrva";
            this.lblPrva.Size = new System.Drawing.Size(51, 20);
            this.lblPrva.TabIndex = 2;
            this.lblPrva.Text = "label1";
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(356, 9);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(51, 20);
            this.lblFilter.TabIndex = 2;
            this.lblFilter.Text = "label1";
            // 
            // cboPrvi
            // 
            this.cboPrvi.FormattingEnabled = true;
            this.cboPrvi.Location = new System.Drawing.Point(12, 32);
            this.cboPrvi.Name = "cboPrvi";
            this.cboPrvi.Size = new System.Drawing.Size(245, 21);
            this.cboPrvi.TabIndex = 3;
            this.cboPrvi.SelectionChangeCommitted += new System.EventHandler(this.cboPrvi_SelectionChangeCommitted);
            // 
            // cboFilter
            // 
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(360, 32);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(245, 21);
            this.cboFilter.TabIndex = 3;
            this.cboFilter.SelectionChangeCommitted += new System.EventHandler(this.cboFilter_SelectionChangeCommitted);
            // 
            // PregledLagera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1166, 641);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.cboPrvi);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblPrva);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PregledLagera";
            this.Text = "PregledLagera";
            this.Load += new System.EventHandler(this.PregledLagera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblPrva;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cboPrvi;
        private System.Windows.Forms.ComboBox cboFilter;
    }
}