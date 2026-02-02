namespace Saobracaj.Drumski
{
    partial class frmStatus
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
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtNalogID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPosaljiStatuse = new System.Windows.Forms.Button();
            this.btnFitriraj = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(0, 0);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(1892, 410);
            this.dataGridView3.TabIndex = 3;
            this.dataGridView3.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView3_CellBeginEdit);
            this.dataGridView3.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellValueChanged);
            this.dataGridView3.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView3_CurrentCellDirtyStateChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFitriraj);
            this.panel1.Controls.Add(this.btnPosaljiStatuse);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtNalogID);
            this.panel1.Location = new System.Drawing.Point(0, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1892, 45);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dataGridView3);
            this.panel2.Location = new System.Drawing.Point(0, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1892, 410);
            this.panel2.TabIndex = 5;
            // 
            // txtNalogID
            // 
            this.txtNalogID.Location = new System.Drawing.Point(91, 13);
            this.txtNalogID.Name = "txtNalogID";
            this.txtNalogID.Size = new System.Drawing.Size(161, 22);
            this.txtNalogID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nalog ID";
            // 
            // btnPosaljiStatuse
            // 
            this.btnPosaljiStatuse.Location = new System.Drawing.Point(431, 12);
            this.btnPosaljiStatuse.Name = "btnPosaljiStatuse";
            this.btnPosaljiStatuse.Size = new System.Drawing.Size(145, 23);
            this.btnPosaljiStatuse.TabIndex = 2;
            this.btnPosaljiStatuse.Text = "Posalji statuse";
            this.btnPosaljiStatuse.UseVisualStyleBackColor = true;
            this.btnPosaljiStatuse.Click += new System.EventHandler(this.btnPosaljiStatuse_Click);
            // 
            // btnFitriraj
            // 
            this.btnFitriraj.Location = new System.Drawing.Point(280, 12);
            this.btnFitriraj.Name = "btnFitriraj";
            this.btnFitriraj.Size = new System.Drawing.Size(145, 23);
            this.btnFitriraj.TabIndex = 3;
            this.btnFitriraj.Text = "Filtriraj";
            this.btnFitriraj.UseVisualStyleBackColor = true;
            this.btnFitriraj.Click += new System.EventHandler(this.btnFitriraj_Click);
            // 
            // frmStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1892, 460);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmStatus";
            this.Text = "frmStatus";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPosaljiStatuse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNalogID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnFitriraj;
    }
}