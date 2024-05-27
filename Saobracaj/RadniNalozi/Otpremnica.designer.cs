
namespace Saobracaj.RadniNalozi
{
    partial class Otpremnica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Otpremnica));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbo_Skladiste = new System.Windows.Forms.ComboBox();
            this.cbo_MestoTroska = new System.Windows.Forms.ComboBox();
            this.cbo_Partner = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbo_Lokacija = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cboVozac = new System.Windows.Forms.ComboBox();
            this.btn_Povuci = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.txt_ID = new System.Windows.Forms.TextBox();
            this.btn_Izaberi = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.cboVozilo = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtVozac = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtVozilo = new System.Windows.Forms.TextBox();
            this.dtpVreme = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtBrojKontejnera = new System.Windows.Forms.TextBox();
            this.btnDodeli = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(193, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Skladiste";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(825, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mesto troska";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 13);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Partner";
            // 
            // cbo_Skladiste
            // 
            this.cbo_Skladiste.FormattingEnabled = true;
            this.cbo_Skladiste.Location = new System.Drawing.Point(185, 28);
            this.cbo_Skladiste.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Skladiste.Name = "cbo_Skladiste";
            this.cbo_Skladiste.Size = new System.Drawing.Size(151, 21);
            this.cbo_Skladiste.TabIndex = 1;
            this.cbo_Skladiste.SelectedIndexChanged += new System.EventHandler(this.cbo_Skladiste_SelectedIndexChanged);
            this.cbo_Skladiste.SelectionChangeCommitted += new System.EventHandler(this.cbo_Skladiste_SelectionChangeCommitted);
            // 
            // cbo_MestoTroska
            // 
            this.cbo_MestoTroska.FormattingEnabled = true;
            this.cbo_MestoTroska.Location = new System.Drawing.Point(828, 29);
            this.cbo_MestoTroska.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_MestoTroska.Name = "cbo_MestoTroska";
            this.cbo_MestoTroska.Size = new System.Drawing.Size(151, 21);
            this.cbo_MestoTroska.TabIndex = 1;
            // 
            // cbo_Partner
            // 
            this.cbo_Partner.FormattingEnabled = true;
            this.cbo_Partner.Location = new System.Drawing.Point(545, 28);
            this.cbo_Partner.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Partner.Name = "cbo_Partner";
            this.cbo_Partner.Size = new System.Drawing.Size(253, 21);
            this.cbo_Partner.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(11, 124);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1315, 387);
            this.dataGridView1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1063, 13);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 40);
            this.button1.TabIndex = 3;
            this.button1.Text = "Sacuvaj otpremnicu";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(386, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Lokacija";
            // 
            // cbo_Lokacija
            // 
            this.cbo_Lokacija.FormattingEnabled = true;
            this.cbo_Lokacija.Location = new System.Drawing.Point(367, 28);
            this.cbo_Lokacija.Margin = new System.Windows.Forms.Padding(2);
            this.cbo_Lokacija.Name = "cbo_Lokacija";
            this.cbo_Lokacija.Size = new System.Drawing.Size(151, 21);
            this.cbo_Lokacija.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 59);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Referent";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 55);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Vozač";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 79);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(228, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // cboVozac
            // 
            this.cboVozac.FormattingEnabled = true;
            this.cboVozac.Location = new System.Drawing.Point(293, 73);
            this.cboVozac.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozac.Name = "cboVozac";
            this.cboVozac.Size = new System.Drawing.Size(225, 21);
            this.cboVozac.TabIndex = 6;
            // 
            // btn_Povuci
            // 
            this.btn_Povuci.Location = new System.Drawing.Point(1028, 68);
            this.btn_Povuci.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Povuci.Name = "btn_Povuci";
            this.btn_Povuci.Size = new System.Drawing.Size(119, 40);
            this.btn_Povuci.TabIndex = 8;
            this.btn_Povuci.Text = "Povuci iz porudzbina";
            this.btn_Povuci.UseVisualStyleBackColor = true;
            this.btn_Povuci.Click += new System.EventHandler(this.btn_Povuci_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.dataGridView3);
            this.panel1.Controls.Add(this.txt_ID);
            this.panel1.Controls.Add(this.btn_Izaberi);
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Location = new System.Drawing.Point(185, 169);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1081, 294);
            this.panel1.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(163, 16);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 24);
            this.button2.TabIndex = 5;
            this.button2.Text = "Nazad";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // dataGridView3
            // 
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(571, 54);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(497, 234);
            this.dataGridView3.TabIndex = 4;
            // 
            // txt_ID
            // 
            this.txt_ID.BackColor = System.Drawing.Color.Bisque;
            this.txt_ID.Enabled = false;
            this.txt_ID.Location = new System.Drawing.Point(9, 20);
            this.txt_ID.Margin = new System.Windows.Forms.Padding(2);
            this.txt_ID.Name = "txt_ID";
            this.txt_ID.Size = new System.Drawing.Size(60, 20);
            this.txt_ID.TabIndex = 2;
            // 
            // btn_Izaberi
            // 
            this.btn_Izaberi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btn_Izaberi.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Izaberi.Location = new System.Drawing.Point(92, 16);
            this.btn_Izaberi.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Izaberi.Name = "btn_Izaberi";
            this.btn_Izaberi.Size = new System.Drawing.Size(56, 24);
            this.btn_Izaberi.TabIndex = 1;
            this.btn_Izaberi.Text = "Izaberi";
            this.btn_Izaberi.UseVisualStyleBackColor = false;
            this.btn_Izaberi.Click += new System.EventHandler(this.btn_Izaberi_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(9, 54);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(548, 234);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(542, 56);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Vozilo";
            // 
            // cboVozilo
            // 
            this.cboVozilo.FormattingEnabled = true;
            this.cboVozilo.Location = new System.Drawing.Point(545, 71);
            this.cboVozilo.Margin = new System.Windows.Forms.Padding(2);
            this.cboVozilo.Name = "cboVozilo";
            this.cboVozilo.Size = new System.Drawing.Size(124, 21);
            this.cboVozilo.TabIndex = 7;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(828, 83);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(57, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Spoljni";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtVozac
            // 
            this.txtVozac.Location = new System.Drawing.Point(722, 59);
            this.txtVozac.Margin = new System.Windows.Forms.Padding(2);
            this.txtVozac.Name = "txtVozac";
            this.txtVozac.Size = new System.Drawing.Size(103, 20);
            this.txtVozac.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(683, 66);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Vozač";
            this.label8.Click += new System.EventHandler(this.label6_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(683, 87);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Vozilo";
            this.label9.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtVozilo
            // 
            this.txtVozilo.Location = new System.Drawing.Point(722, 82);
            this.txtVozilo.Margin = new System.Windows.Forms.Padding(2);
            this.txtVozilo.Name = "txtVozilo";
            this.txtVozilo.Size = new System.Drawing.Size(103, 20);
            this.txtVozilo.TabIndex = 11;
            // 
            // dtpVreme
            // 
            this.dtpVreme.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpVreme.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpVreme.Location = new System.Drawing.Point(896, 79);
            this.dtpVreme.Name = "dtpVreme";
            this.dtpVreme.ShowUpDown = true;
            this.dtpVreme.Size = new System.Drawing.Size(127, 20);
            this.dtpVreme.TabIndex = 19;
            this.dtpVreme.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(919, 63);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Datum i vreme";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 13);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Broj kontejnera";
            // 
            // txtBrojKontejnera
            // 
            this.txtBrojKontejnera.Location = new System.Drawing.Point(9, 29);
            this.txtBrojKontejnera.Name = "txtBrojKontejnera";
            this.txtBrojKontejnera.Size = new System.Drawing.Size(158, 20);
            this.txtBrojKontejnera.TabIndex = 21;
            // 
            // btnDodeli
            // 
            this.btnDodeli.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDodeli.Location = new System.Drawing.Point(750, 516);
            this.btnDodeli.Name = "btnDodeli";
            this.btnDodeli.Size = new System.Drawing.Size(254, 23);
            this.btnDodeli.TabIndex = 22;
            this.btnDodeli.Text = "^ ^ ^ ^\r\n";
            this.btnDodeli.UseVisualStyleBackColor = true;
            this.btnDodeli.Click += new System.EventHandler(this.btnDodeli_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(12, 545);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(1311, 80);
            this.dataGridView4.TabIndex = 23;
            // 
            // Otpremnica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1335, 637);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.txtBrojKontejnera);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpVreme);
            this.Controls.Add(this.btn_Povuci);
            this.Controls.Add(this.cbo_MestoTroska);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboVozac);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboVozilo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbo_Partner);
            this.Controls.Add(this.cbo_Lokacija);
            this.Controls.Add(this.cbo_Skladiste);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVozilo);
            this.Controls.Add(this.txtVozac);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnDodeli);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Otpremnica";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Otpremnica robe";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Otpremnica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_Skladiste;
        private System.Windows.Forms.ComboBox cbo_MestoTroska;
        private System.Windows.Forms.ComboBox cbo_Partner;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbo_Lokacija;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox cboVozac;
        private System.Windows.Forms.Button btn_Povuci;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Izaberi;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboVozilo;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txtVozac;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtVozilo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dtpVreme;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtBrojKontejnera;
        private System.Windows.Forms.Button btnDodeli;
        private System.Windows.Forms.DataGridView dataGridView4;
    }
}