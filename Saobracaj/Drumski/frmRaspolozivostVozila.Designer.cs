namespace Saobracaj.Drumski
{
    partial class frmRaspolozivostVozila
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDatumS = new System.Windows.Forms.CheckBox();
            this.chkDatumD = new System.Windows.Forms.CheckBox();
            this.btnFiltriraj = new System.Windows.Forms.Button();
            this.cboRegistracija = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboPrevoznik = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 71);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1245, 379);
            this.dataGridView1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFiltriraj);
            this.panel1.Controls.Add(this.cboPrevoznik);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboRegistracija);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.chkDatumD);
            this.panel1.Controls.Add(this.chkDatumS);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1245, 450);
            this.panel1.TabIndex = 1;
            // 
            // chkDatumS
            // 
            this.chkDatumS.AutoSize = true;
            this.chkDatumS.Location = new System.Drawing.Point(125, 12);
            this.chkDatumS.Name = "chkDatumS";
            this.chkDatumS.Size = new System.Drawing.Size(60, 20);
            this.chkDatumS.TabIndex = 19;
            this.chkDatumS.Text = "Sutra";
            this.chkDatumS.UseVisualStyleBackColor = true;
            this.chkDatumS.CheckedChanged += new System.EventHandler(this.ChkDatum_CheckedChanged);
            // 
            // chkDatumD
            // 
            this.chkDatumD.AutoSize = true;
            this.chkDatumD.Location = new System.Drawing.Point(17, 12);
            this.chkDatumD.Name = "chkDatumD";
            this.chkDatumD.Size = new System.Drawing.Size(69, 20);
            this.chkDatumD.TabIndex = 18;
            this.chkDatumD.Text = "Danas";
            this.chkDatumD.UseVisualStyleBackColor = true;
            this.chkDatumD.CheckedChanged += new System.EventHandler(this.ChkDatum_CheckedChanged);
            // 
            // btnFiltriraj
            // 
            this.btnFiltriraj.Location = new System.Drawing.Point(631, 39);
            this.btnFiltriraj.Name = "btnFiltriraj";
            this.btnFiltriraj.Size = new System.Drawing.Size(94, 24);
            this.btnFiltriraj.TabIndex = 11;
            this.btnFiltriraj.Text = "Filtriraj";
            this.btnFiltriraj.Click += new System.EventHandler(this.btnFiltriraj_Click);
            // 
            // cboRegistracija
            // 
            this.cboRegistracija.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRegistracija.Location = new System.Drawing.Point(122, 39);
            this.cboRegistracija.Name = "cboRegistracija";
            this.cboRegistracija.Size = new System.Drawing.Size(185, 24);
            this.cboRegistracija.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 39);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(66, 22);
            this.label3.TabIndex = 13;
            this.label3.Text = "Prevoznik";
            // 
            // cboPrevoznik
            // 
            this.cboPrevoznik.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrevoznik.Location = new System.Drawing.Point(414, 39);
            this.cboPrevoznik.Name = "cboPrevoznik";
            this.cboPrevoznik.Size = new System.Drawing.Size(185, 24);
            this.cboPrevoznik.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 39);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(100, 22);
            this.label4.TabIndex = 14;
            this.label4.Text = "Broj registracije";
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // frmRaspolozivostVozila
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1245, 450);
            this.Controls.Add(this.panel1);
            this.Name = "frmRaspolozivostVozila";
            this.Text = "frmRaspolozivostVozila";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDatumS;
        private System.Windows.Forms.CheckBox chkDatumD;
        private System.Windows.Forms.Button btnFiltriraj;
        private System.Windows.Forms.ComboBox cboPrevoznik;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboRegistracija;
        private System.Windows.Forms.Label label4;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
    }
}