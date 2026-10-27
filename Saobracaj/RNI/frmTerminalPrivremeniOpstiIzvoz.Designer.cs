namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniOpstiIzvoz
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPolja = new System.Windows.Forms.Label();
            this.clbPolja = new System.Windows.Forms.CheckedListBox();
            this.btnOznaciSve = new System.Windows.Forms.Button();
            this.btnPonistiSve = new System.Windows.Forms.Button();
            this.lblZapisa = new System.Windows.Forms.Label();
            this.btnIzvezi = new System.Windows.Forms.Button();
            this.btnZatvori = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.groupBox1.Controls.Add(this.lblPolja);
            this.groupBox1.Controls.Add(this.clbPolja);
            this.groupBox1.Controls.Add(this.btnOznaciSve);
            this.groupBox1.Controls.Add(this.btnPonistiSve);
            this.groupBox1.Controls.Add(this.lblZapisa);
            this.groupBox1.Controls.Add(this.btnIzvezi);
            this.groupBox1.Controls.Add(this.btnZatvori);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(420, 560);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Polja koja idu u Excel";
            //
            // lblPolja
            //
            this.lblPolja.AutoSize = true;
            this.lblPolja.Location = new System.Drawing.Point(15, 24);
            this.lblPolja.Name = "lblPolja";
            this.lblPolja.Size = new System.Drawing.Size(175, 13);
            this.lblPolja.TabIndex = 0;
            this.lblPolja.Text = "Štiklirajte polja koja želite da izvezete";
            //
            // clbPolja
            //
            this.clbPolja.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbPolja.CheckOnClick = true;
            this.clbPolja.FormattingEnabled = true;
            this.clbPolja.IntegralHeight = false;
            this.clbPolja.Location = new System.Drawing.Point(15, 42);
            this.clbPolja.Name = "clbPolja";
            this.clbPolja.Size = new System.Drawing.Size(390, 380);
            this.clbPolja.TabIndex = 1;
            //
            // btnOznaciSve
            //
            this.btnOznaciSve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOznaciSve.Location = new System.Drawing.Point(15, 430);
            this.btnOznaciSve.Name = "btnOznaciSve";
            this.btnOznaciSve.Size = new System.Drawing.Size(120, 28);
            this.btnOznaciSve.TabIndex = 2;
            this.btnOznaciSve.Text = "Označi sve";
            this.btnOznaciSve.UseVisualStyleBackColor = true;
            this.btnOznaciSve.Click += new System.EventHandler(this.btnOznaciSve_Click);
            //
            // btnPonistiSve
            //
            this.btnPonistiSve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPonistiSve.Location = new System.Drawing.Point(145, 430);
            this.btnPonistiSve.Name = "btnPonistiSve";
            this.btnPonistiSve.Size = new System.Drawing.Size(120, 28);
            this.btnPonistiSve.TabIndex = 3;
            this.btnPonistiSve.Text = "Poništi sve";
            this.btnPonistiSve.UseVisualStyleBackColor = true;
            this.btnPonistiSve.Click += new System.EventHandler(this.btnPonistiSve_Click);
            //
            // lblZapisa
            //
            this.lblZapisa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblZapisa.AutoSize = true;
            this.lblZapisa.Location = new System.Drawing.Point(15, 472);
            this.lblZapisa.Name = "lblZapisa";
            this.lblZapisa.Size = new System.Drawing.Size(160, 13);
            this.lblZapisa.TabIndex = 4;
            this.lblZapisa.Text = "Zapisa za izvoz (posle filtera): 0";
            //
            // btnIzvezi
            //
            this.btnIzvezi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIzvezi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnIzvezi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIzvezi.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnIzvezi.Location = new System.Drawing.Point(15, 505);
            this.btnIzvezi.Name = "btnIzvezi";
            this.btnIzvezi.Size = new System.Drawing.Size(190, 34);
            this.btnIzvezi.TabIndex = 5;
            this.btnIzvezi.Text = "Izvezi u Excel";
            this.btnIzvezi.UseVisualStyleBackColor = false;
            this.btnIzvezi.Click += new System.EventHandler(this.btnIzvezi_Click);
            //
            // btnZatvori
            //
            this.btnZatvori.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnZatvori.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnZatvori.Location = new System.Drawing.Point(215, 505);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(190, 34);
            this.btnZatvori.TabIndex = 6;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.UseVisualStyleBackColor = true;
            //
            // frmTerminalPrivremeniOpstiIzvoz
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnZatvori;
            this.ClientSize = new System.Drawing.Size(420, 560);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(436, 400);
            this.Name = "frmTerminalPrivremeniOpstiIzvoz";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opšti izvoz";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPolja;
        private System.Windows.Forms.CheckedListBox clbPolja;
        private System.Windows.Forms.Button btnOznaciSve;
        private System.Windows.Forms.Button btnPonistiSve;
        private System.Windows.Forms.Label lblZapisa;
        private System.Windows.Forms.Button btnIzvezi;
        private System.Windows.Forms.Button btnZatvori;
    }
}
