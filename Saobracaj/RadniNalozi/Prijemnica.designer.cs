
namespace Saobracaj.RadniNalozi
{
    partial class Prijemnica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prijemnica));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.cbo_Partner = new System.Windows.Forms.ComboBox();
            this.cbo_MestoTroska = new System.Windows.Forms.ComboBox();
            this.cbo_Skladiste = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_Lokacija = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbo_Referent = new System.Windows.Forms.ComboBox();
            this.btn_Povuci = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btn_Izaberi = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cboPrimio = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dtpVreme = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(9, 111);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1055, 525);
            this.dataGridView1.TabIndex = 9;
            // 
            // cbo_Partner
            // 
            this.cbo_Partner.FormattingEnabled = true;
            this.cbo_Partner.Location = new System.Drawing.Point(548, 26);
            this.cbo_Partner.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Partner.Name = "cbo_Partner";
            this.cbo_Partner.Size = new System.Drawing.Size(282, 21);
            this.cbo_Partner.TabIndex = 6;
            // 
            // cbo_MestoTroska
            // 
            this.cbo_MestoTroska.FormattingEnabled = true;
            this.cbo_MestoTroska.Location = new System.Drawing.Point(380, 26);
            this.cbo_MestoTroska.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_MestoTroska.Name = "cbo_MestoTroska";
            this.cbo_MestoTroska.Size = new System.Drawing.Size(151, 21);
            this.cbo_MestoTroska.TabIndex = 7;
            // 
            // cbo_Skladiste
            // 
            this.cbo_Skladiste.FormattingEnabled = true;
            this.cbo_Skladiste.Location = new System.Drawing.Point(9, 26);
            this.cbo_Skladiste.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Skladiste.Name = "cbo_Skladiste";
            this.cbo_Skladiste.Size = new System.Drawing.Size(151, 21);
            this.cbo_Skladiste.TabIndex = 8;
            this.cbo_Skladiste.SelectionChangeCommitted += new System.EventHandler(this.cbo_Skladiste_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(546, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Partner";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(378, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mesto troska";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Skladiste";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(972, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 42);
            this.button1.TabIndex = 10;
            this.button1.Text = "Sacuvaj prijemnicu";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Lokacija";
            // 
            // cbo_Lokacija
            // 
            this.cbo_Lokacija.FormattingEnabled = true;
            this.cbo_Lokacija.Location = new System.Drawing.Point(190, 26);
            this.cbo_Lokacija.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Lokacija.Name = "cbo_Lokacija";
            this.cbo_Lokacija.Size = new System.Drawing.Size(151, 21);
            this.cbo_Lokacija.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 67);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Referent";
            // 
            // cbo_Referent
            // 
            this.cbo_Referent.FormattingEnabled = true;
            this.cbo_Referent.Location = new System.Drawing.Point(9, 87);
            this.cbo_Referent.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Referent.Name = "cbo_Referent";
            this.cbo_Referent.Size = new System.Drawing.Size(252, 21);
            this.cbo_Referent.TabIndex = 6;
            // 
            // btn_Povuci
            // 
            this.btn_Povuci.Location = new System.Drawing.Point(861, 26);
            this.btn_Povuci.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Povuci.Name = "btn_Povuci";
            this.btn_Povuci.Size = new System.Drawing.Size(100, 40);
            this.btn_Povuci.TabIndex = 8;
            this.btn_Povuci.Text = "Povuci iz narudžbina";
            this.btn_Povuci.UseVisualStyleBackColor = true;
            this.btn_Povuci.Visible = false;
            this.btn_Povuci.Click += new System.EventHandler(this.btn_Povuci_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txt_ID);
            this.panel1.Controls.Add(this.dataGridView3);
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Controls.Add(this.btn_Izaberi);
            this.panel1.Location = new System.Drawing.Point(26, 135);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1055, 632);
            this.panel1.TabIndex = 11;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(163, 9);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "Nazad";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // txt_ID
            // 
            this.txt_ID.BackColor = System.Drawing.Color.Bisque;
            this.txt_ID.Enabled = false;
            this.txt_ID.Location = new System.Drawing.Point(15, 15);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(60, 20);
            this.txt_ID.TabIndex = 2;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(567, 38);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(477, 561);
            this.dataGridView3.TabIndex = 0;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(8, 38);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(542, 561);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // btn_Izaberi
            // 
            this.btn_Izaberi.Location = new System.Drawing.Point(94, 9);
            this.btn_Izaberi.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Izaberi.Name = "btn_Izaberi";
            this.btn_Izaberi.Size = new System.Drawing.Size(56, 24);
            this.btn_Izaberi.TabIndex = 1;
            this.btn_Izaberi.Text = "Izaberi";
            this.btn_Izaberi.UseVisualStyleBackColor = true;
            this.btn_Izaberi.Click += new System.EventHandler(this.btn_Izaberi_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(368, 67);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Primio";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // cboPrimio
            // 
            this.cboPrimio.FormattingEnabled = true;
            this.cboPrimio.Location = new System.Drawing.Point(370, 87);
            this.cboPrimio.Margin = new System.Windows.Forms.Padding(2);
            this.cboPrimio.Name = "cboPrimio";
            this.cboPrimio.Size = new System.Drawing.Size(252, 21);
            this.cboPrimio.TabIndex = 6;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(653, 67);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Datum i vreme";
            // 
            // dtpVreme
            // 
            this.dtpVreme.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpVreme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVreme.Location = new System.Drawing.Point(656, 83);
            this.dtpVreme.Name = "dtpVreme";
            this.dtpVreme.ShowUpDown = true;
            this.dtpVreme.Size = new System.Drawing.Size(118, 20);
            this.dtpVreme.TabIndex = 21;
            this.dtpVreme.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // Prijemnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1073, 646);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpVreme);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Povuci);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboPrimio);
            this.Controls.Add(this.cbo_Referent);
            this.Controls.Add(this.cbo_Partner);
            this.Controls.Add(this.cbo_Lokacija);
            this.Controls.Add(this.cbo_MestoTroska);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbo_Skladiste);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Prijemnica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prijemnica";
            this.Load += new System.EventHandler(this.Prijemnica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox cbo_Partner;
        private System.Windows.Forms.ComboBox cbo_MestoTroska;
        private System.Windows.Forms.ComboBox cbo_Skladiste;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo_Lokacija;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbo_Referent;
        private System.Windows.Forms.Button btn_Povuci;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button btn_Izaberi;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboPrimio;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtpVreme;
    }
}