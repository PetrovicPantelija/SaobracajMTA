namespace Saobracaj.Pantheon_Export
{
    partial class Opportunity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Opportunity));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.cboOdeljenje = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProizvod = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProductFor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboKlijent = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKontaktOsoba = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboOpportunity = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboJob = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBudzet = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboValuta = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEstimated = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOpis = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboStartPoint = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboEndPoint = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(743, 27);
            this.toolStrip1.TabIndex = 437;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 24);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 24);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 24);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(106, 24);
            this.toolStripButton1.Text = "Opportunity-Won";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(103, 24);
            this.toolStripButton2.Text = "Opportunity-Lost";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 438;
            this.label1.Text = "Naziv posla";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(556, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 439;
            this.label2.Text = "Odeljenje";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Location = new System.Drawing.Point(12, 52);
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(434, 20);
            this.txtNaziv.TabIndex = 440;
            // 
            // cboOdeljenje
            // 
            this.cboOdeljenje.FormattingEnabled = true;
            this.cboOdeljenje.Location = new System.Drawing.Point(461, 52);
            this.cboOdeljenje.Name = "cboOdeljenje";
            this.cboOdeljenje.Size = new System.Drawing.Size(261, 21);
            this.cboOdeljenje.TabIndex = 441;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 439;
            this.label3.Text = "Product";
            // 
            // txtProizvod
            // 
            this.txtProizvod.Location = new System.Drawing.Point(12, 98);
            this.txtProizvod.Name = "txtProizvod";
            this.txtProizvod.Size = new System.Drawing.Size(148, 20);
            this.txtProizvod.TabIndex = 442;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 443;
            this.label4.Text = "Product for";
            // 
            // txtProductFor
            // 
            this.txtProductFor.Location = new System.Drawing.Point(182, 98);
            this.txtProductFor.Name = "txtProductFor";
            this.txtProductFor.Size = new System.Drawing.Size(209, 20);
            this.txtProductFor.TabIndex = 444;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(556, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 443;
            this.label5.Text = "Klijent";
            // 
            // cboKlijent
            // 
            this.cboKlijent.FormattingEnabled = true;
            this.cboKlijent.Location = new System.Drawing.Point(397, 97);
            this.cboKlijent.Name = "cboKlijent";
            this.cboKlijent.Size = new System.Drawing.Size(325, 21);
            this.cboKlijent.TabIndex = 445;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(75, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 443;
            this.label6.Text = "Kontakt osoba";
            // 
            // txtKontaktOsoba
            // 
            this.txtKontaktOsoba.Location = new System.Drawing.Point(12, 151);
            this.txtKontaktOsoba.Name = "txtKontaktOsoba";
            this.txtKontaktOsoba.Size = new System.Drawing.Size(229, 20);
            this.txtKontaktOsoba.TabIndex = 446;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(333, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 443;
            this.label7.Text = "Opportunity type";
            // 
            // cboOpportunity
            // 
            this.cboOpportunity.FormattingEnabled = true;
            this.cboOpportunity.Items.AddRange(new object[] {
            "Project",
            "Tender",
            "Standard"});
            this.cboOpportunity.Location = new System.Drawing.Point(269, 150);
            this.cboOpportunity.Name = "cboOpportunity";
            this.cboOpportunity.Size = new System.Drawing.Size(196, 21);
            this.cboOpportunity.TabIndex = 445;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(606, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 443;
            this.label8.Text = "Job type";
            // 
            // cboJob
            // 
            this.cboJob.FormattingEnabled = true;
            this.cboJob.Items.AddRange(new object[] {
            "Export",
            "Import",
            "Transit",
            "Local",
            "Third countris"});
            this.cboJob.Location = new System.Drawing.Point(532, 150);
            this.cboJob.Name = "cboJob";
            this.cboJob.Size = new System.Drawing.Size(190, 21);
            this.cboJob.TabIndex = 445;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(59, 186);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 443;
            this.label9.Text = "Budzet";
            // 
            // txtBudzet
            // 
            this.txtBudzet.Location = new System.Drawing.Point(12, 202);
            this.txtBudzet.Name = "txtBudzet";
            this.txtBudzet.Size = new System.Drawing.Size(123, 20);
            this.txtBudzet.TabIndex = 447;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(266, 185);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 443;
            this.label10.Text = "Valuta";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // cboValuta
            // 
            this.cboValuta.FormattingEnabled = true;
            this.cboValuta.Location = new System.Drawing.Point(182, 201);
            this.cboValuta.Name = "cboValuta";
            this.cboValuta.Size = new System.Drawing.Size(210, 21);
            this.cboValuta.TabIndex = 448;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(448, 186);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 13);
            this.label11.TabIndex = 443;
            this.label11.Text = "Estimated revenue";
            // 
            // txtEstimated
            // 
            this.txtEstimated.Location = new System.Drawing.Point(423, 202);
            this.txtEstimated.Name = "txtEstimated";
            this.txtEstimated.Size = new System.Drawing.Size(157, 20);
            this.txtEstimated.TabIndex = 447;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 241);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 443;
            this.label12.Text = "Opis posla";
            // 
            // txtOpis
            // 
            this.txtOpis.Location = new System.Drawing.Point(11, 257);
            this.txtOpis.Multiline = true;
            this.txtOpis.Name = "txtOpis";
            this.txtOpis.Size = new System.Drawing.Size(222, 67);
            this.txtOpis.TabIndex = 447;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(618, 201);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(74, 17);
            this.checkBox1.TabIndex = 449;
            this.checkBox1.Text = "Won/Lost";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(337, 257);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 13);
            this.label13.TabIndex = 443;
            this.label13.Text = "Start point";
            // 
            // cboStartPoint
            // 
            this.cboStartPoint.FormattingEnabled = true;
            this.cboStartPoint.Location = new System.Drawing.Point(239, 274);
            this.cboStartPoint.Name = "cboStartPoint";
            this.cboStartPoint.Size = new System.Drawing.Size(234, 21);
            this.cboStartPoint.TabIndex = 448;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(601, 257);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 443;
            this.label14.Text = "End point";
            // 
            // cboEndPoint
            // 
            this.cboEndPoint.FormattingEnabled = true;
            this.cboEndPoint.Location = new System.Drawing.Point(488, 274);
            this.cboEndPoint.Name = "cboEndPoint";
            this.cboEndPoint.Size = new System.Drawing.Size(234, 21);
            this.cboEndPoint.TabIndex = 448;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(11, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(715, 321);
            this.panel1.TabIndex = 450;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 451;
            this.button1.Text = "Izaberi";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(709, 281);
            this.dataGridView1.TabIndex = 450;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Opportunity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 363);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboEndPoint);
            this.Controls.Add(this.txtOpis);
            this.Controls.Add(this.cboJob);
            this.Controls.Add(this.cboKlijent);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboOdeljenje);
            this.Controls.Add(this.txtNaziv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProizvod);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtKontaktOsoba);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtBudzet);
            this.Controls.Add(this.txtProductFor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboOpportunity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboValuta);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtEstimated);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cboStartPoint);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Opportunity";
            this.Text = "Opportunity";
            this.Load += new System.EventHandler(this.Opportunity_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.ComboBox cboOdeljenje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProizvod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProductFor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboKlijent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKontaktOsoba;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboOpportunity;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboJob;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBudzet;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboValuta;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEstimated;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOpis;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboStartPoint;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboEndPoint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}