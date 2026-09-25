namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniPromeni
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
            this.lblPolje = new System.Windows.Forms.Label();
            this.cboPolje = new System.Windows.Forms.ComboBox();
            this.lblNovaVrednost = new System.Windows.Forms.Label();
            this.txtOpsti = new System.Windows.Forms.TextBox();
            this.dtpOpsti = new System.Windows.Forms.DateTimePicker();
            this.cboOpsti = new System.Windows.Forms.ComboBox();
            this.lblOznaceno = new System.Windows.Forms.Label();
            this.btnPrimeni = new System.Windows.Forms.Button();
            this.btnZatvori = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.groupBox1.Controls.Add(this.lblPolje);
            this.groupBox1.Controls.Add(this.cboPolje);
            this.groupBox1.Controls.Add(this.lblNovaVrednost);
            this.groupBox1.Controls.Add(this.txtOpsti);
            this.groupBox1.Controls.Add(this.dtpOpsti);
            this.groupBox1.Controls.Add(this.cboOpsti);
            this.groupBox1.Controls.Add(this.lblOznaceno);
            this.groupBox1.Controls.Add(this.btnPrimeni);
            this.groupBox1.Controls.Add(this.btnZatvori);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(620, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Promena vrednosti na više selektovanih stavki";
            //
            // lblPolje
            //
            this.lblPolje.AutoSize = true;
            this.lblPolje.Location = new System.Drawing.Point(15, 28);
            this.lblPolje.Name = "lblPolje";
            this.lblPolje.Size = new System.Drawing.Size(30, 13);
            this.lblPolje.TabIndex = 0;
            this.lblPolje.Text = "Polje";
            //
            // cboPolje
            //
            this.cboPolje.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPolje.FormattingEnabled = true;
            this.cboPolje.Location = new System.Drawing.Point(15, 46);
            this.cboPolje.Name = "cboPolje";
            this.cboPolje.Size = new System.Drawing.Size(260, 21);
            this.cboPolje.TabIndex = 1;
            this.cboPolje.SelectedIndexChanged += new System.EventHandler(this.cboPolje_SelectedIndexChanged);
            //
            // lblNovaVrednost
            //
            this.lblNovaVrednost.AutoSize = true;
            this.lblNovaVrednost.Location = new System.Drawing.Point(295, 28);
            this.lblNovaVrednost.Name = "lblNovaVrednost";
            this.lblNovaVrednost.Size = new System.Drawing.Size(77, 13);
            this.lblNovaVrednost.TabIndex = 2;
            this.lblNovaVrednost.Text = "Nova vrednost";
            //
            // txtOpsti
            //
            this.txtOpsti.Location = new System.Drawing.Point(295, 46);
            this.txtOpsti.Name = "txtOpsti";
            this.txtOpsti.Size = new System.Drawing.Size(305, 20);
            this.txtOpsti.TabIndex = 3;
            this.txtOpsti.Visible = false;
            //
            // dtpOpsti
            //
            this.dtpOpsti.Checked = false;
            this.dtpOpsti.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpOpsti.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOpsti.Location = new System.Drawing.Point(295, 46);
            this.dtpOpsti.Name = "dtpOpsti";
            this.dtpOpsti.ShowCheckBox = true;
            this.dtpOpsti.Size = new System.Drawing.Size(305, 20);
            this.dtpOpsti.TabIndex = 4;
            this.dtpOpsti.Visible = false;
            //
            // cboOpsti
            //
            this.cboOpsti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOpsti.FormattingEnabled = true;
            this.cboOpsti.Location = new System.Drawing.Point(295, 46);
            this.cboOpsti.Name = "cboOpsti";
            this.cboOpsti.Size = new System.Drawing.Size(305, 21);
            this.cboOpsti.TabIndex = 5;
            this.cboOpsti.Visible = false;
            //
            // lblOznaceno
            //
            this.lblOznaceno.AutoSize = true;
            this.lblOznaceno.Location = new System.Drawing.Point(15, 88);
            this.lblOznaceno.Name = "lblOznaceno";
            this.lblOznaceno.Size = new System.Drawing.Size(100, 13);
            this.lblOznaceno.TabIndex = 6;
            this.lblOznaceno.Text = "Označeno zapisa: 0";
            //
            // btnPrimeni
            //
            this.btnPrimeni.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnPrimeni.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimeni.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnPrimeni.Location = new System.Drawing.Point(15, 125);
            this.btnPrimeni.Name = "btnPrimeni";
            this.btnPrimeni.Size = new System.Drawing.Size(260, 34);
            this.btnPrimeni.TabIndex = 7;
            this.btnPrimeni.Text = "Primeni";
            this.btnPrimeni.UseVisualStyleBackColor = false;
            this.btnPrimeni.Click += new System.EventHandler(this.btnPrimeni_Click);
            //
            // btnZatvori
            //
            this.btnZatvori.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnZatvori.Location = new System.Drawing.Point(295, 125);
            this.btnZatvori.Name = "btnZatvori";
            this.btnZatvori.Size = new System.Drawing.Size(305, 34);
            this.btnZatvori.TabIndex = 8;
            this.btnZatvori.Text = "Zatvori";
            this.btnZatvori.UseVisualStyleBackColor = true;
            //
            // frmTerminalPrivremeniPromeni
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnZatvori;
            this.ClientSize = new System.Drawing.Size(620, 180);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTerminalPrivremeniPromeni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grupna promena";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPolje;
        private System.Windows.Forms.ComboBox cboPolje;
        private System.Windows.Forms.Label lblNovaVrednost;
        private System.Windows.Forms.TextBox txtOpsti;
        private System.Windows.Forms.DateTimePicker dtpOpsti;
        private System.Windows.Forms.ComboBox cboOpsti;
        private System.Windows.Forms.Label lblOznaceno;
        private System.Windows.Forms.Button btnPrimeni;
        private System.Windows.Forms.Button btnZatvori;
    }
}
