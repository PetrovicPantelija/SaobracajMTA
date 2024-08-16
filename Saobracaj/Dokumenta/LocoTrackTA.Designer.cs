namespace Saobracaj.Dokumenta
{
    partial class LocoTrackTA
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.rbMaxSpeed = new System.Windows.Forms.RadioButton();
            this.rbSignals = new System.Windows.Forms.RadioButton();
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.btnPozicije = new System.Windows.Forms.Button();
            this.btnLokomotiva = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.cboPosao = new System.Windows.Forms.ComboBox();
            this.cboLokomotiva = new System.Windows.Forms.ComboBox();
            this.btnPosao = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(334, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(900, 724);
            this.webBrowser1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(2, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(330, 724);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Gainsboro;
            this.tabPage4.Controls.Add(this.btnPosao);
            this.tabPage4.Controls.Add(this.dataGridView1);
            this.tabPage4.Controls.Add(this.label36);
            this.tabPage4.Controls.Add(this.label35);
            this.tabPage4.Controls.Add(this.label34);
            this.tabPage4.Controls.Add(this.label33);
            this.tabPage4.Controls.Add(this.rbMaxSpeed);
            this.tabPage4.Controls.Add(this.rbSignals);
            this.tabPage4.Controls.Add(this.rbStandard);
            this.tabPage4.Controls.Add(this.btnPozicije);
            this.tabPage4.Controls.Add(this.btnLokomotiva);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.label30);
            this.tabPage4.Controls.Add(this.cboPosao);
            this.tabPage4.Controls.Add(this.cboLokomotiva);
            this.tabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(322, 698);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Pretraga";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(313, 282);
            this.dataGridView1.TabIndex = 21;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(77, 463);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(108, 20);
            this.label36.TabIndex = 20;
            this.label36.Text = "Detaljni prikaz";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(87, 623);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(223, 30);
            this.label35.TabIndex = 17;
            this.label35.Text = "Visualization of railway line maxspeeds \r\nand speed signals";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(87, 565);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(226, 30);
            this.label34.TabIndex = 18;
            this.label34.Text = "Visualization of railway signals and train \r\nprotection systems";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(86, 492);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(212, 45);
            this.label33.TabIndex = 19;
            this.label33.Text = "Default railway layer visualizing \r\ninfrastructure such as tracks, stations, \r\nli" +
    "ne numbers, switches, etc.";
            // 
            // rbMaxSpeed
            // 
            this.rbMaxSpeed.AutoSize = true;
            this.rbMaxSpeed.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbMaxSpeed.Location = new System.Drawing.Point(3, 629);
            this.rbMaxSpeed.Name = "rbMaxSpeed";
            this.rbMaxSpeed.Size = new System.Drawing.Size(85, 19);
            this.rbMaxSpeed.TabIndex = 14;
            this.rbMaxSpeed.TabStop = true;
            this.rbMaxSpeed.Text = "MaxSpeed";
            this.rbMaxSpeed.UseVisualStyleBackColor = true;
            // 
            // rbSignals
            // 
            this.rbSignals.AutoSize = true;
            this.rbSignals.ForeColor = System.Drawing.SystemColors.Highlight;
            this.rbSignals.Location = new System.Drawing.Point(3, 573);
            this.rbSignals.Name = "rbSignals";
            this.rbSignals.Size = new System.Drawing.Size(66, 19);
            this.rbSignals.TabIndex = 15;
            this.rbSignals.TabStop = true;
            this.rbSignals.Text = "Signals";
            this.rbSignals.UseVisualStyleBackColor = true;
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rbStandard.Location = new System.Drawing.Point(6, 506);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(75, 19);
            this.rbStandard.TabIndex = 16;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Standard";
            this.rbStandard.UseVisualStyleBackColor = true;
            // 
            // btnPozicije
            // 
            this.btnPozicije.BackColor = System.Drawing.Color.Gray;
            this.btnPozicije.ForeColor = System.Drawing.Color.White;
            this.btnPozicije.Location = new System.Drawing.Point(6, 404);
            this.btnPozicije.Name = "btnPozicije";
            this.btnPozicije.Size = new System.Drawing.Size(107, 42);
            this.btnPozicije.TabIndex = 13;
            this.btnPozicije.Text = "Pozicije";
            this.btnPozicije.UseVisualStyleBackColor = false;
            this.btnPozicije.Click += new System.EventHandler(this.btnPozicije_Click);
            // 
            // btnLokomotiva
            // 
            this.btnLokomotiva.BackColor = System.Drawing.Color.Gray;
            this.btnLokomotiva.ForeColor = System.Drawing.Color.White;
            this.btnLokomotiva.Location = new System.Drawing.Point(119, 404);
            this.btnLokomotiva.Name = "btnLokomotiva";
            this.btnLokomotiva.Size = new System.Drawing.Size(95, 42);
            this.btnLokomotiva.TabIndex = 12;
            this.btnLokomotiva.Text = "Po lokomotivi";
            this.btnLokomotiva.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 344);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Posao";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(5, 288);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(70, 15);
            this.label30.TabIndex = 11;
            this.label30.Text = "Lokomotiva";
            // 
            // cboPosao
            // 
            this.cboPosao.FormattingEnabled = true;
            this.cboPosao.Location = new System.Drawing.Point(5, 362);
            this.cboPosao.Name = "cboPosao";
            this.cboPosao.Size = new System.Drawing.Size(231, 23);
            this.cboPosao.TabIndex = 8;
            // 
            // cboLokomotiva
            // 
            this.cboLokomotiva.FormattingEnabled = true;
            this.cboLokomotiva.Location = new System.Drawing.Point(5, 306);
            this.cboLokomotiva.Name = "cboLokomotiva";
            this.cboLokomotiva.Size = new System.Drawing.Size(231, 23);
            this.cboLokomotiva.TabIndex = 9;
            // 
            // btnPosao
            // 
            this.btnPosao.BackColor = System.Drawing.Color.Gray;
            this.btnPosao.ForeColor = System.Drawing.Color.White;
            this.btnPosao.Location = new System.Drawing.Point(220, 404);
            this.btnPosao.Name = "btnPosao";
            this.btnPosao.Size = new System.Drawing.Size(95, 42);
            this.btnPosao.TabIndex = 22;
            this.btnPosao.Text = "Po poslu";
            this.btnPosao.UseVisualStyleBackColor = false;
            // 
            // LocoTrackTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 729);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.webBrowser1);
            this.Name = "LocoTrackTA";
            this.Text = "LocoTrack";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LocoTrackTA_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.RadioButton rbMaxSpeed;
        private System.Windows.Forms.RadioButton rbSignals;
        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.Button btnPozicije;
        private System.Windows.Forms.Button btnLokomotiva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cboPosao;
        private System.Windows.Forms.ComboBox cboLokomotiva;
        private System.Windows.Forms.Button btnPosao;
    }
}