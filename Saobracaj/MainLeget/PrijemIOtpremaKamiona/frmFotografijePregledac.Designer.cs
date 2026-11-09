namespace Saobracaj.MainLeget.PrijemIOtpremaKamiona
{
    partial class frmFotografijePregledac
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
            this.button5 = new System.Windows.Forms.Button();
            this.txtObjedinjen = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.btnSacuvajTara = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTara = new System.Windows.Forms.TextBox();
            this.btnTovarniList = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPlomba = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKontejner = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSifraNajave = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.xlsIOConfig1 = new Syncfusion.XlsIO.XlsIOConfig();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.button5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button5.Location = new System.Drawing.Point(584, 205);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(61, 20);
            this.button5.TabIndex = 190;
            this.button5.Text = "Otvori";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // txtObjedinjen
            // 
            this.txtObjedinjen.Location = new System.Drawing.Point(237, 206);
            this.txtObjedinjen.Name = "txtObjedinjen";
            this.txtObjedinjen.Size = new System.Drawing.Size(341, 20);
            this.txtObjedinjen.TabIndex = 189;
            // 
            // button12
            // 
            this.button12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button12.Location = new System.Drawing.Point(14, 200);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(217, 31);
            this.button12.TabIndex = 182;
            this.button12.Text = "Dokumentacija pripremljena formiraj pdf ";
            this.button12.UseVisualStyleBackColor = false;
            // 
            // btnSacuvajTara
            // 
            this.btnSacuvajTara.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnSacuvajTara.Enabled = false;
            this.btnSacuvajTara.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSacuvajTara.Location = new System.Drawing.Point(576, 45);
            this.btnSacuvajTara.Name = "btnSacuvajTara";
            this.btnSacuvajTara.Size = new System.Drawing.Size(61, 20);
            this.btnSacuvajTara.TabIndex = 173;
            this.btnSacuvajTara.Text = "Sačuvaj";
            this.btnSacuvajTara.UseVisualStyleBackColor = false;
            this.btnSacuvajTara.Click += new System.EventHandler(this.btnSacuvajTovarniList_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 172;
            this.label5.Text = "Tara";
            // 
            // txtTara
            // 
            this.txtTara.Enabled = false;
            this.txtTara.Location = new System.Drawing.Point(92, 49);
            this.txtTara.Name = "txtTara";
            this.txtTara.Size = new System.Drawing.Size(322, 20);
            this.txtTara.TabIndex = 170;
            // 
            // btnTovarniList
            // 
            this.btnTovarniList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.btnTovarniList.Enabled = false;
            this.btnTovarniList.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTovarniList.Location = new System.Drawing.Point(420, 48);
            this.btnTovarniList.Name = "btnTovarniList";
            this.btnTovarniList.Size = new System.Drawing.Size(59, 20);
            this.btnTovarniList.TabIndex = 171;
            this.btnTovarniList.Text = "Pronađi";
            this.btnTovarniList.UseVisualStyleBackColor = false;
            this.btnTovarniList.Click += new System.EventHandler(this.btnTovarniList_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button6.Enabled = false;
            this.button6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button6.Location = new System.Drawing.Point(576, 71);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(61, 20);
            this.button6.TabIndex = 169;
            this.button6.Text = "Sačuvaj";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(576, 105);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(61, 20);
            this.button4.TabIndex = 168;
            this.button4.Text = "Sačuvaj";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 167;
            this.label2.Text = "Plomba";
            // 
            // txtPlomba
            // 
            this.txtPlomba.Enabled = false;
            this.txtPlomba.Location = new System.Drawing.Point(92, 75);
            this.txtPlomba.Name = "txtPlomba";
            this.txtPlomba.Size = new System.Drawing.Size(322, 20);
            this.txtPlomba.TabIndex = 165;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button3.Enabled = false;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(420, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(59, 20);
            this.button3.TabIndex = 166;
            this.button3.Text = "Pronađi";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(496, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 20);
            this.button2.TabIndex = 164;
            this.button2.Text = "Otvori";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(226)))), ((int)(((byte)(230)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 237);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(997, 213);
            this.dataGridView1.TabIndex = 163;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 162;
            this.label1.Text = "Kontejner";
            // 
            // txtKontejner
            // 
            this.txtKontejner.Location = new System.Drawing.Point(92, 106);
            this.txtKontejner.Name = "txtKontejner";
            this.txtKontejner.Size = new System.Drawing.Size(322, 20);
            this.txtKontejner.TabIndex = 158;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(204)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(421, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 20);
            this.button1.TabIndex = 159;
            this.button1.Text = "Pronađi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSifra
            // 
            this.txtSifra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSifra.Location = new System.Drawing.Point(93, 20);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(56, 20);
            this.txtSifra.TabIndex = 156;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 161;
            this.label4.Text = "Šifra";
            // 
            // txtSifraNajave
            // 
            this.txtSifraNajave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtSifraNajave.Location = new System.Drawing.Point(237, 20);
            this.txtSifraNajave.Name = "txtSifraNajave";
            this.txtSifraNajave.Size = new System.Drawing.Size(86, 20);
            this.txtSifraNajave.TabIndex = 157;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 160;
            this.label3.Text = "RNI";
            // 
            // ofd1
            // 
            this.ofd1.FileName = "openFileDialog1";
            // 
            // frmFotografijePregledac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 450);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtObjedinjen);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.btnSacuvajTara);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTara);
            this.Controls.Add(this.btnTovarniList);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPlomba);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKontejner);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSifraNajave);
            this.Controls.Add(this.label3);
            this.Name = "frmFotografijePregledac";
            this.Text = "frmFotografijePregledac";
            this.Load += new System.EventHandler(this.frmFotografijePregledac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txtObjedinjen;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button btnSacuvajTara;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtTara;
        private System.Windows.Forms.Button btnTovarniList;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPlomba;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKontejner;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSifraNajave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog ofd1;
        private Syncfusion.XlsIO.XlsIOConfig xlsIOConfig1;
        private System.Windows.Forms.FolderBrowserDialog fbd;
    }
}