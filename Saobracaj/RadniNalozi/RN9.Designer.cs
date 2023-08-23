
namespace Saobracaj.RadniNalozi
{
    partial class RN9
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
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.txtDatumRealizacije = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNAvoznosredstvo = new System.Windows.Forms.Label();
            this.txtNalogIzdao = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDatumRasporeda = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox7
            // 
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.ItemHeight = 13;
            this.comboBox7.Location = new System.Drawing.Point(234, 181);
            this.comboBox7.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(186, 21);
            this.comboBox7.TabIndex = 385;
            // 
            // txtDatumRealizacije
            // 
            this.txtDatumRealizacije.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRealizacije.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRealizacije.Location = new System.Drawing.Point(232, 218);
            this.txtDatumRealizacije.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatumRealizacije.Name = "txtDatumRealizacije";
            this.txtDatumRealizacije.Size = new System.Drawing.Size(122, 20);
            this.txtDatumRealizacije.TabIndex = 361;
            this.txtDatumRealizacije.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(127, 218);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 360;
            this.label9.Text = "Datum realizacije";
            // 
            // txtNAvoznosredstvo
            // 
            this.txtNAvoznosredstvo.AutoSize = true;
            this.txtNAvoznosredstvo.Location = new System.Drawing.Point(127, 184);
            this.txtNAvoznosredstvo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNAvoznosredstvo.Name = "txtNAvoznosredstvo";
            this.txtNAvoznosredstvo.Size = new System.Drawing.Size(102, 13);
            this.txtNAvoznosredstvo.TabIndex = 359;
            this.txtNAvoznosredstvo.Text = "SA voznog sredstva";
            // 
            // txtNalogIzdao
            // 
            this.txtNalogIzdao.FormattingEnabled = true;
            this.txtNalogIzdao.ItemHeight = 13;
            this.txtNalogIzdao.Location = new System.Drawing.Point(230, 144);
            this.txtNalogIzdao.Margin = new System.Windows.Forms.Padding(2);
            this.txtNalogIzdao.Name = "txtNalogIzdao";
            this.txtNalogIzdao.Size = new System.Drawing.Size(186, 21);
            this.txtNalogIzdao.TabIndex = 358;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(127, 144);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 357;
            this.label6.Text = "Nalog izdao";
            // 
            // txtDatumRasporeda
            // 
            this.txtDatumRasporeda.CustomFormat = "dd.MM.yyyy HH:mm";
            this.txtDatumRasporeda.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtDatumRasporeda.Location = new System.Drawing.Point(232, 103);
            this.txtDatumRasporeda.Margin = new System.Windows.Forms.Padding(2);
            this.txtDatumRasporeda.Name = "txtDatumRasporeda";
            this.txtDatumRasporeda.Size = new System.Drawing.Size(122, 20);
            this.txtDatumRasporeda.TabIndex = 356;
            this.txtDatumRasporeda.Value = new System.DateTime(2023, 6, 23, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 355;
            this.label1.Text = "Datum rasporeda";
            // 
            // RN9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 681);
            this.Controls.Add(this.comboBox7);
            this.Controls.Add(this.txtDatumRealizacije);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtNAvoznosredstvo);
            this.Controls.Add(this.txtNalogIzdao);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDatumRasporeda);
            this.Controls.Add(this.label1);
            this.Name = "RN9";
            this.Text = "RN9";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.DateTimePicker txtDatumRealizacije;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label txtNAvoznosredstvo;
        private System.Windows.Forms.ComboBox txtNalogIzdao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker txtDatumRasporeda;
        private System.Windows.Forms.Label label1;
    }
}