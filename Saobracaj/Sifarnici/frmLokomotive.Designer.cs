namespace Saobracaj.Sifarnici
{
    partial class frmLokomotive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLokomotive));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkAktivna = new System.Windows.Forms.CheckBox();
            this.chkDizel = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLokomotiva = new System.Windows.Forms.TextBox();
            this.btnRacun = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMasa = new System.Windows.Forms.NumericUpDown();
            this.cboSerija = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMasa)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Lokomotiva šifra";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(732, 239);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // chkAktivna
            // 
            this.chkAktivna.AutoSize = true;
            this.chkAktivna.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAktivna.Location = new System.Drawing.Point(8, 148);
            this.chkAktivna.Name = "chkAktivna";
            this.chkAktivna.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAktivna.Size = new System.Drawing.Size(70, 20);
            this.chkAktivna.TabIndex = 188;
            this.chkAktivna.Text = "Aktivna";
            this.chkAktivna.UseVisualStyleBackColor = true;
            // 
            // chkDizel
            // 
            this.chkDizel.AutoSize = true;
            this.chkDizel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDizel.Location = new System.Drawing.Point(106, 148);
            this.chkDizel.Name = "chkDizel";
            this.chkDizel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDizel.Size = new System.Drawing.Size(56, 20);
            this.chkDizel.TabIndex = 199;
            this.chkDizel.Text = "Dizel";
            this.chkDizel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 201;
            this.label5.Text = "Lozinka:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(8, 124);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(128, 20);
            this.txtPassword.TabIndex = 200;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLokomotiva
            // 
            this.txtLokomotiva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.txtLokomotiva.ForeColor = System.Drawing.Color.White;
            this.txtLokomotiva.Location = new System.Drawing.Point(13, 59);
            this.txtLokomotiva.Name = "txtLokomotiva";
            this.txtLokomotiva.Size = new System.Drawing.Size(124, 20);
            this.txtLokomotiva.TabIndex = 202;
            this.txtLokomotiva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnRacun
            // 
            this.btnRacun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnRacun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRacun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRacun.ForeColor = System.Drawing.Color.White;
            this.btnRacun.Location = new System.Drawing.Point(592, 138);
            this.btnRacun.Name = "btnRacun";
            this.btnRacun.Size = new System.Drawing.Size(152, 27);
            this.btnRacun.TabIndex = 203;
            this.btnRacun.Text = "Promeni podatke";
            this.btnRacun.UseVisualStyleBackColor = false;
            this.btnRacun.Click += new System.EventHandler(this.btnRacun_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(362, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 16);
            this.label4.TabIndex = 205;
            this.label4.Text = "Masa teretnog vozila:";
            // 
            // txtMasa
            // 
            this.txtMasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtMasa.Location = new System.Drawing.Point(365, 124);
            this.txtMasa.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMasa.Name = "txtMasa";
            this.txtMasa.Size = new System.Drawing.Size(90, 21);
            this.txtMasa.TabIndex = 204;
            // 
            // cboSerija
            // 
            this.cboSerija.FormattingEnabled = true;
            this.cboSerija.Location = new System.Drawing.Point(365, 59);
            this.cboSerija.Name = "cboSerija";
            this.cboSerija.Size = new System.Drawing.Size(152, 21);
            this.cboSerija.TabIndex = 207;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(369, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 208;
            this.label7.Text = "Serija";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(420, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 209;
            this.label9.Text = "kg";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(756, 27);
            this.toolStrip1.TabIndex = 210;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Margin = new System.Windows.Forms.Padding(5, 1, 0, 2);
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(160, 59);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(189, 20);
            this.txtNaziv.TabIndex = 211;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(158, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 16);
            this.label2.TabIndex = 212;
            this.label2.Text = "Naziv";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(109, 24);
            this.toolStripButton1.Text = "Aktivne/Neaktivne";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // frmLokomotive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(756, 443);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboSerija);
            this.Controls.Add(this.txtMasa);
            this.Controls.Add(this.btnRacun);
            this.Controls.Add(this.txtLokomotiva);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkDizel);
            this.Controls.Add(this.chkAktivna);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLokomotive";
            this.Text = "Lokomotive";
            this.Load += new System.EventHandler(this.frmLokomotive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMasa)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkAktivna;
        private System.Windows.Forms.CheckBox chkDizel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLokomotiva;
        private System.Windows.Forms.Button btnRacun;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtMasa;
        private System.Windows.Forms.ComboBox cboSerija;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}