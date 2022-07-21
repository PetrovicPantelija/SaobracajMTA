namespace Testiranje.Izvestaji
{
    partial class frmFakturisanjeMan
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
            this.btnUcitaj = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.txtDatumOd = new System.Windows.Forms.DateTimePicker();
            this.txtDatumDo = new System.Windows.Forms.DateTimePicker();
            this.cboKomitent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUcitaj
            // 
            this.btnUcitaj.Location = new System.Drawing.Point(972, 37);
            this.btnUcitaj.Name = "btnUcitaj";
            this.btnUcitaj.Size = new System.Drawing.Size(123, 27);
            this.btnUcitaj.TabIndex = 100;
            this.btnUcitaj.Text = "Učitaj";
            this.btnUcitaj.UseVisualStyleBackColor = true;
            this.btnUcitaj.Click += new System.EventHandler(this.btnUcitaj_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 17);
            this.label1.TabIndex = 99;
            this.label1.Text = "Izaberite komitenta:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TrackModal.Izvestaji.rptFakturisanjeManipulacija.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 86);
            this.reportViewer1.Name = "reportViewer1";
         //   this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1084, 508);
            this.reportViewer1.TabIndex = 101;
            // 
            // txtDatumOd
            // 
            this.txtDatumOd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDatumOd.Location = new System.Drawing.Point(332, 43);
            this.txtDatumOd.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.txtDatumOd.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.txtDatumOd.Name = "txtDatumOd";
            this.txtDatumOd.Size = new System.Drawing.Size(200, 20);
            this.txtDatumOd.TabIndex = 102;
            // 
            // txtDatumDo
            // 
            this.txtDatumDo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDatumDo.Location = new System.Drawing.Point(652, 40);
            this.txtDatumDo.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.txtDatumDo.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.txtDatumDo.Name = "txtDatumDo";
            this.txtDatumDo.Size = new System.Drawing.Size(200, 20);
            this.txtDatumDo.TabIndex = 103;
            // 
            // cboKomitent
            // 
            this.cboKomitent.FormattingEnabled = true;
            this.cboKomitent.Location = new System.Drawing.Point(12, 43);
            this.cboKomitent.Name = "cboKomitent";
            this.cboKomitent.Size = new System.Drawing.Size(200, 21);
            this.cboKomitent.TabIndex = 104;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(649, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 105;
            this.label2.Text = "Datum do:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(329, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 106;
            this.label3.Text = "Datum od:";
            // 
            // frmFakturisanjeMan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1108, 606);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboKomitent);
            this.Controls.Add(this.txtDatumDo);
            this.Controls.Add(this.txtDatumOd);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.btnUcitaj);
            this.Controls.Add(this.label1);
            this.Name = "frmFakturisanjeMan";
            this.Text = "Fakturisanje manipulacija";
            this.Load += new System.EventHandler(this.frmFakturisanjeMan_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUcitaj;
        private System.Windows.Forms.Label label1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DateTimePicker txtDatumOd;
        private System.Windows.Forms.DateTimePicker txtDatumDo;
        private System.Windows.Forms.ComboBox cboKomitent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}