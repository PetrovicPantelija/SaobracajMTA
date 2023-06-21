
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cboPosao
            // 
            this.cboPosao.FormattingEnabled = true;
            this.cboPosao.Location = new System.Drawing.Point(141, 33);
            this.cboPosao.Name = "cboPosao";
            this.cboPosao.Size = new System.Drawing.Size(192, 21);
            this.cboPosao.TabIndex = 220;
            this.cboPosao.SelectedIndexChanged += new System.EventHandler(this.cboPosao_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(348, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(186, 42);
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
            this.dataGridView1.Location = new System.Drawing.Point(10, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1111, 409);
            this.dataGridView1.TabIndex = 218;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnPretrazi
            // 
            this.btnPretrazi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.btnPretrazi.ForeColor = System.Drawing.Color.White;
            this.btnPretrazi.Location = new System.Drawing.Point(10, 11);
            this.btnPretrazi.Name = "btnPretrazi";
            this.btnPretrazi.Size = new System.Drawing.Size(111, 42);
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
            this.button2.Location = new System.Drawing.Point(548, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 42);
            this.button2.TabIndex = 222;
            this.button2.Text = "Otvori najavu";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(661, 11);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 42);
            this.button3.TabIndex = 223;
            this.button3.Text = "Otvori TrainList";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(773, 11);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(106, 42);
            this.button4.TabIndex = 224;
            this.button4.Text = "Otvori Teretnice";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(495, 59);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(106, 42);
            this.button5.TabIndex = 225;
            this.button5.Text = "Tehnicki pregled";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(620, 59);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(106, 42);
            this.button6.TabIndex = 226;
            this.button6.Text = "Komercijalni pregled";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(773, 62);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(106, 42);
            this.button7.TabIndex = 227;
            this.button7.Text = "Primopredaja";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(886, 62);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(106, 42);
            this.button8.TabIndex = 228;
            this.button8.Text = "Vuca";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(125)))), ((int)(((byte)(81)))));
            this.button9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button9.Location = new System.Drawing.Point(372, 59);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(106, 42);
            this.button9.TabIndex = 229;
            this.button9.Text = "Automobili";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // txt_Sifra
            // 
            this.txt_Sifra.Location = new System.Drawing.Point(94, 84);
            this.txt_Sifra.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            // frmPregledAktivnosti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1130, 530);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmPregledAktivnosti";
            this.Text = "Pregled aktivnosti";
            this.Load += new System.EventHandler(this.frmPregledAktivnosti_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
    }
}