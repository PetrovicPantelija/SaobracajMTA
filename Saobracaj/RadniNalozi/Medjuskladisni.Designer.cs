
namespace Saobracaj.RadniNalozi
{
    partial class Medjuskladisni
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbo_LokacijaIZ = new System.Windows.Forms.ComboBox();
            this.cbo_SkladisteIZ = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbo_LokacijaU = new System.Windows.Forms.ComboBox();
            this.cbo_SkladisteU = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cboVozac = new System.Windows.Forms.ComboBox();
            this.cboVozilo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpVreme = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.cbo_LokacijaIZ);
            this.panel1.Controls.Add(this.cbo_SkladisteIZ);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(9, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 189);
            this.panel1.TabIndex = 0;
            // 
            // cbo_LokacijaIZ
            // 
            this.cbo_LokacijaIZ.FormattingEnabled = true;
            this.cbo_LokacijaIZ.Location = new System.Drawing.Point(2, 110);
            this.cbo_LokacijaIZ.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_LokacijaIZ.Name = "cbo_LokacijaIZ";
            this.cbo_LokacijaIZ.Size = new System.Drawing.Size(202, 21);
            this.cbo_LokacijaIZ.TabIndex = 2;
            // 
            // cbo_SkladisteIZ
            // 
            this.cbo_SkladisteIZ.FormattingEnabled = true;
            this.cbo_SkladisteIZ.Location = new System.Drawing.Point(2, 58);
            this.cbo_SkladisteIZ.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_SkladisteIZ.Name = "cbo_SkladisteIZ";
            this.cbo_SkladisteIZ.Size = new System.Drawing.Size(202, 21);
            this.cbo_SkladisteIZ.TabIndex = 2;
            this.cbo_SkladisteIZ.SelectionChangeCommitted += new System.EventHandler(this.cbo_SkladisteIZ_SelectionChangeCommitted);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Lokacija";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Skladiste";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Iz skladista:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.cbo_LokacijaU);
            this.panel2.Controls.Add(this.cbo_SkladisteU);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(250, 10);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(206, 189);
            this.panel2.TabIndex = 0;
            // 
            // cbo_LokacijaU
            // 
            this.cbo_LokacijaU.FormattingEnabled = true;
            this.cbo_LokacijaU.Location = new System.Drawing.Point(2, 110);
            this.cbo_LokacijaU.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_LokacijaU.Name = "cbo_LokacijaU";
            this.cbo_LokacijaU.Size = new System.Drawing.Size(202, 21);
            this.cbo_LokacijaU.TabIndex = 2;
            // 
            // cbo_SkladisteU
            // 
            this.cbo_SkladisteU.FormattingEnabled = true;
            this.cbo_SkladisteU.Location = new System.Drawing.Point(2, 54);
            this.cbo_SkladisteU.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_SkladisteU.Name = "cbo_SkladisteU";
            this.cbo_SkladisteU.Size = new System.Drawing.Size(202, 21);
            this.cbo_SkladisteU.TabIndex = 2;
            this.cbo_SkladisteU.SelectionChangeCommitted += new System.EventHandler(this.cbo_SkladisteU_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 94);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Lokacija";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "U skladiste:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 38);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Skladiste";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(560, 216);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Prenesi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 257);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(634, 373);
            this.dataGridView1.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 213);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Vozač";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(254, 213);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Vozilo";
            // 
            // cboVozac
            // 
            this.cboVozac.FormattingEnabled = true;
            this.cboVozac.Location = new System.Drawing.Point(9, 232);
            this.cboVozac.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozac.Name = "cboVozac";
            this.cboVozac.Size = new System.Drawing.Size(206, 21);
            this.cboVozac.TabIndex = 4;
            // 
            // cboVozilo
            // 
            this.cboVozilo.FormattingEnabled = true;
            this.cboVozilo.Location = new System.Drawing.Point(250, 232);
            this.cboVozilo.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozilo.Name = "cboVozilo";
            this.cboVozilo.Size = new System.Drawing.Size(110, 21);
            this.cboVozilo.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(480, 10);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Datum i vreme";
            // 
            // dtpVreme
            // 
            this.dtpVreme.CustomFormat = "yyyy-MM-dd hh:mm";
            this.dtpVreme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVreme.Location = new System.Drawing.Point(461, 26);
            this.dtpVreme.Name = "dtpVreme";
            this.dtpVreme.ShowUpDown = true;
            this.dtpVreme.Size = new System.Drawing.Size(127, 20);
            this.dtpVreme.TabIndex = 21;
            this.dtpVreme.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(460, 64);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Napomena:";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Location = new System.Drawing.Point(461, 80);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(181, 102);
            this.txtNapomena.TabIndex = 23;
            // 
            // Medjuskladisni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 639);
            this.Controls.Add(this.txtNapomena);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpVreme);
            this.Controls.Add(this.cboVozilo);
            this.Controls.Add(this.cboVozac);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Medjuskladisni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medjuskladisni";
            this.Load += new System.EventHandler(this.Medjuskladisni_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbo_SkladisteIZ;
        private System.Windows.Forms.ComboBox cbo_LokacijaIZ;
        private System.Windows.Forms.ComboBox cbo_LokacijaU;
        private System.Windows.Forms.ComboBox cbo_SkladisteU;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboVozac;
        private System.Windows.Forms.ComboBox cboVozilo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpVreme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNapomena;
    }
}