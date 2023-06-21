
namespace Saobracaj.TrackModal.Sifarnici
{
    partial class frmKopiranjeCenovnikaPoTipu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKopiranjeCenovnikaPoTipu));
            this.label6 = new System.Windows.Forms.Label();
            this.cboTipCenovnikaIz = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTipCenovnikaU = new System.Windows.Forms.ComboBox();
            this.cboRazred = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(23, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 107;
            this.label6.Text = "Tip cenovnika iz:";
            // 
            // cboTipCenovnikaIz
            // 
            this.cboTipCenovnikaIz.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboTipCenovnikaIz.FormattingEnabled = true;
            this.cboTipCenovnikaIz.Location = new System.Drawing.Point(126, 41);
            this.cboTipCenovnikaIz.Name = "cboTipCenovnikaIz";
            this.cboTipCenovnikaIz.Size = new System.Drawing.Size(148, 24);
            this.cboTipCenovnikaIz.TabIndex = 106;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(300, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 109;
            this.label1.Text = "Tip cenovnika u:";
            // 
            // cboTipCenovnikaU
            // 
            this.cboTipCenovnikaU.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboTipCenovnikaU.FormattingEnabled = true;
            this.cboTipCenovnikaU.Location = new System.Drawing.Point(401, 41);
            this.cboTipCenovnikaU.Name = "cboTipCenovnikaU";
            this.cboTipCenovnikaU.Size = new System.Drawing.Size(148, 24);
            this.cboTipCenovnikaU.TabIndex = 108;
            // 
            // cboRazred
            // 
            this.cboRazred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboRazred.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboRazred.FormattingEnabled = true;
            this.cboRazred.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H"});
            this.cboRazred.Location = new System.Drawing.Point(155, 92);
            this.cboRazred.Name = "cboRazred";
            this.cboRazred.Size = new System.Drawing.Size(167, 24);
            this.cboRazred.TabIndex = 130;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(14, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 15);
            this.label11.TabIndex = 131;
            this.label11.Text = "Razred cenovnika";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(328, 92);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 25);
            this.button3.TabIndex = 129;
            this.button3.Text = "Pretraži";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(595, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(349, 25);
            this.button1.TabIndex = 126;
            this.button1.Text = "Kopiraj iz tipa u tip";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(15, 150);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1013, 478);
            this.dataGridView1.TabIndex = 132;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDelete,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1058, 27);
            this.toolStrip1.TabIndex = 133;
            this.toolStrip1.Text = "Štampaj izveštaj";
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(449, 92);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(257, 25);
            this.button4.TabIndex = 135;
            this.button4.Text = "Kopiranje selektovanog";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmKopiranjeCenovnikaPoTipu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1058, 640);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboRazred);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTipCenovnikaU);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTipCenovnikaIz);
            this.Name = "frmKopiranjeCenovnikaPoTipu";
            this.Text = "Kopiranje cenovnika po parametrima";
            this.Load += new System.EventHandler(this.frmKopiranjeCenovnikaPoTipu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboTipCenovnikaIz;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTipCenovnikaU;
        private System.Windows.Forms.ComboBox cboRazred;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button button4;
    }
}