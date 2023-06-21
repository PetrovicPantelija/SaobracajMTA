
namespace Saobracaj.Dokumenta
{
    partial class frmPregledLokomotivaPrimopredaja
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
            this.btn_OtvoriSliku = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_OtvoriSliku
            // 
            this.btn_OtvoriSliku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btn_OtvoriSliku.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_OtvoriSliku.Location = new System.Drawing.Point(11, 11);
            this.btn_OtvoriSliku.Margin = new System.Windows.Forms.Padding(2);
            this.btn_OtvoriSliku.Name = "btn_OtvoriSliku";
            this.btn_OtvoriSliku.Size = new System.Drawing.Size(114, 28);
            this.btn_OtvoriSliku.TabIndex = 221;
            this.btn_OtvoriSliku.Text = "Refresh";
            this.btn_OtvoriSliku.UseVisualStyleBackColor = false;
            this.btn_OtvoriSliku.Click += new System.EventHandler(this.btn_OtvoriSliku_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 59);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1212, 682);
            this.dataGridView1.TabIndex = 220;
            // 
            // frmPregledLokomotivaPrimopredaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 771);
            this.Controls.Add(this.btn_OtvoriSliku);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmPregledLokomotivaPrimopredaja";
            this.Text = "frmPregledLokomotivaPrimopredaja";
            this.Load += new System.EventHandler(this.frmPregledLokomotivaPrimopredaja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OtvoriSliku;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}