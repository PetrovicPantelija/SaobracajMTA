
namespace Saobracaj.Dokumenta
{
    partial class frmPregledAktivnosti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPregledAktivnosti));
            this.cboPosao = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnPretrazi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.txt_Sifra = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkAktvni = new System.Windows.Forms.CheckBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPutanja = new System.Windows.Forms.TextBox();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSifraNajave = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.fbd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkAutomobili = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // cboPosao
            // 
            this.cboPosao.FormattingEnabled = true;
            this.cboPosao.Location = new System.Drawing.Point(140, 27);
            this.cboPosao.Name = "cboPosao";
            this.cboPosao.Size = new System.Drawing.Size(192, 21);
            this.cboPosao.TabIndex = 220;
            this.cboPosao.SelectedIndexChanged += new System.EventHandler(this.cboPosao_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(454, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 42);
            this.button1.TabIndex = 219;
            this.button1.Text = "Aktivnosti po poslu";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 419);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1478, 324);
            this.dataGridView1.TabIndex = 218;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnPretrazi
            // 
            this.btnPretrazi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnPretrazi.ForeColor = System.Drawing.Color.White;
            this.btnPretrazi.Location = new System.Drawing.Point(10, 11);
            this.btnPretrazi.Name = "btnPretrazi";
            this.btnPretrazi.Size = new System.Drawing.Size(122, 42);
            this.btnPretrazi.TabIndex = 217;
            this.btnPretrazi.Text = "Sve aktivnosti";
            this.btnPretrazi.UseVisualStyleBackColor = false;
            this.btnPretrazi.Click += new System.EventHandler(this.btnPretrazi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 221;
            this.label1.Text = "Posao";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(10, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 42);
            this.button2.TabIndex = 222;
            this.button2.Text = "Otvori prevoz";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(122, 109);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 42);
            this.button3.TabIndex = 223;
            this.button3.Text = "Otvori TrainList";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(234, 109);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 42);
            this.button4.TabIndex = 224;
            this.button4.Text = "Otvori Teretnice";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(513, 63);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(118, 42);
            this.button5.TabIndex = 225;
            this.button5.Text = "Tehnicki pregled";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(372, 109);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(135, 42);
            this.button6.TabIndex = 226;
            this.button6.Text = "Komercijalni pregled";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(513, 111);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(120, 42);
            this.button7.TabIndex = 227;
            this.button7.Text = "LO-dnevni pregled";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(637, 62);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(124, 42);
            this.button8.TabIndex = 228;
            this.button8.Text = "Vuca";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button9.Location = new System.Drawing.Point(372, 62);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(135, 42);
            this.button9.TabIndex = 229;
            this.button9.Text = "Automobili";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // txt_Sifra
            // 
            this.txt_Sifra.Location = new System.Drawing.Point(94, 84);
            this.txt_Sifra.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Sifra.Name = "txt_Sifra";
            this.txt_Sifra.Size = new System.Drawing.Size(72, 20);
            this.txt_Sifra.TabIndex = 231;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 84);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 13);
            this.label15.TabIndex = 230;
            this.label15.Text = "Šifra aktivnosti:";
            // 
            // chkAktvni
            // 
            this.chkAktvni.AutoSize = true;
            this.chkAktvni.Checked = true;
            this.chkAktvni.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAktvni.Location = new System.Drawing.Point(339, 15);
            this.chkAktvni.Name = "chkAktvni";
            this.chkAktvni.Size = new System.Drawing.Size(95, 17);
            this.chkAktvni.TabIndex = 232;
            this.chkAktvni.Text = "Tekuci poslovi";
            this.chkAktvni.UseVisualStyleBackColor = true;
            this.chkAktvni.Click += new System.EventHandler(this.chkAktvni_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(10, 188);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1475, 225);
            this.dataGridView2.TabIndex = 233;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(792, 62);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(693, 120);
            this.dataGridView3.TabIndex = 234;
            this.dataGridView3.SelectionChanged += new System.EventHandler(this.dataGridView3_SelectionChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(789, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 239;
            this.label2.Text = "Ostala dok";
            // 
            // txtPutanja
            // 
            this.txtPutanja.Location = new System.Drawing.Point(874, 34);
            this.txtPutanja.Name = "txtPutanja";
            this.txtPutanja.Size = new System.Drawing.Size(322, 20);
            this.txtPutanja.TabIndex = 238;
            // 
            // txtSifra
            // 
            this.txtSifra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSifra.Location = new System.Drawing.Point(792, 8);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(56, 20);
            this.txtSifra.TabIndex = 235;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(871, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 237;
            this.label4.Text = "Šifra aktivnosti";
            // 
            // txtSifraNajave
            // 
            this.txtSifraNajave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSifraNajave.Location = new System.Drawing.Point(953, 8);
            this.txtSifraNajave.Name = "txtSifraNajave";
            this.txtSifraNajave.Size = new System.Drawing.Size(86, 20);
            this.txtSifraNajave.TabIndex = 236;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(1218, 33);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(61, 20);
            this.button10.TabIndex = 240;
            this.button10.Text = "Otvori";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // ofd1
            // 
            this.ofd1.FileName = "ofd1";
            // 
            // chkAutomobili
            // 
            this.chkAutomobili.AutoSize = true;
            this.chkAutomobili.Checked = true;
            this.chkAutomobili.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutomobili.Location = new System.Drawing.Point(339, 34);
            this.chkAutomobili.Name = "chkAutomobili";
            this.chkAutomobili.Size = new System.Drawing.Size(109, 17);
            this.chkAutomobili.TabIndex = 241;
            this.chkAutomobili.Text = "Automobili/Ostalo";
            this.chkAutomobili.UseVisualStyleBackColor = true;
            // 
            // frmPregledAktivnosti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1497, 754);
            this.Controls.Add(this.chkAutomobili);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPutanja);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSifraNajave);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.chkAktvni);
            this.Controls.Add(this.txt_Sifra);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPosao);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnPretrazi);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPregledAktivnosti";
            this.Text = "Pregled aktivnosti";
            this.Load += new System.EventHandler(this.frmPregledAktivnosti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPosao;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnPretrazi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txt_Sifra;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkAktvni;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPutanja;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSifraNajave;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private System.Windows.Forms.FolderBrowserDialog fbd1;
        private System.Windows.Forms.CheckBox chkAutomobili;
    }
}