namespace Saobracaj.Dokumenta
{
    partial class frmPrijemnica
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
            this.label5 = new System.Windows.Forms.Label();
            this.cboPrijemnica = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnPrihvatiIzmene = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "Prijemnica:";
            // 
            // cboPrijemnica
            // 
            this.cboPrijemnica.FormattingEnabled = true;
            this.cboPrijemnica.Location = new System.Drawing.Point(76, 23);
            this.cboPrijemnica.Name = "cboPrijemnica";
            this.cboPrijemnica.Size = new System.Drawing.Size(141, 21);
            this.cboPrijemnica.TabIndex = 35;
            this.cboPrijemnica.Leave += new System.EventHandler(this.cboPrijemnica_Leave);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(969, 303);
            this.dataGridView1.TabIndex = 37;
            // 
            // btnPrihvatiIzmene
            // 
            this.btnPrihvatiIzmene.Location = new System.Drawing.Point(255, 23);
            this.btnPrihvatiIzmene.Name = "btnPrihvatiIzmene";
            this.btnPrihvatiIzmene.Size = new System.Drawing.Size(136, 23);
            this.btnPrihvatiIzmene.TabIndex = 38;
            this.btnPrihvatiIzmene.Text = "Prihvati Izmene";
            this.btnPrihvatiIzmene.UseVisualStyleBackColor = true;
            this.btnPrihvatiIzmene.Click += new System.EventHandler(this.btnPrihvatiIzmene_Click);
            // 
            // frmPrijemnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 356);
            this.Controls.Add(this.btnPrihvatiIzmene);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboPrijemnica);
            this.Name = "frmPrijemnica";
            this.Text = "main";
            this.Load += new System.EventHandler(this.frmPrijemnica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPrijemnica;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnPrihvatiIzmene;
    }
}