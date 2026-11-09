namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniExcel
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
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.btnOtvoriFajl = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ofdDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.panelHeader.Size = new System.Drawing.Size(1200, 33);
            this.panelHeader.TabIndex = 461;
            //
            // panel2
            //
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnUcitaj);
            this.panel2.Controls.Add(this.btnOtvoriFajl);
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1190, 31);
            this.panel2.TabIndex = 6;
            //
            // btnUcitaj
            //
            this.btnUcitaj.AutoSize = true;
            this.btnUcitaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUcitaj.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnUcitaj.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnUcitaj.FlatAppearance.BorderSize = 0;
            this.btnUcitaj.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnUcitaj.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnUcitaj.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUcitaj.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnUcitaj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnUcitaj.Location = new System.Drawing.Point(120, 0);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(100, 31);
            this.btnUcitaj.TabIndex = 2;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUcitaj.UseVisualStyleBackColor = true;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            //
            // btnOtvoriFajl
            //
            this.btnOtvoriFajl.AutoSize = true;
            this.btnOtvoriFajl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOtvoriFajl.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnOtvoriFajl.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnOtvoriFajl.FlatAppearance.BorderSize = 0;
            this.btnOtvoriFajl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnOtvoriFajl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnOtvoriFajl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtvoriFajl.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnOtvoriFajl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnOtvoriFajl.Location = new System.Drawing.Point(0, 0);
            this.btnOtvoriFajl.Name = "btnOtvoriFajl";
            this.btnOtvoriFajl.Size = new System.Drawing.Size(120, 31);
            this.btnOtvoriFajl.TabIndex = 1;
            this.btnOtvoriFajl.Text = "Otvori fajl";
            this.btnOtvoriFajl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOtvoriFajl.UseVisualStyleBackColor = true;
            this.btnOtvoriFajl.Click += new System.EventHandler(this.btnOtvoriFajl_Click);
            //
            // dataGridView1
            //
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1200, 567);
            this.dataGridView1.TabIndex = 1;
            //
            // ofdDialog
            //
            this.ofdDialog.Filter = "Excel (*.xlsx;*.xls)|*.xlsx;*.xls|Svi fajlovi (*.*)|*.*";
            this.ofdDialog.Title = "Izaberite Excel fajl";
            //
            // frmTerminalPrivremeniExcel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1200, 600);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmTerminalPrivremeniExcel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Terminal privremeni - uvoz Excel";
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.Button btnOtvoriFajl;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog ofdDialog;
    }
}
