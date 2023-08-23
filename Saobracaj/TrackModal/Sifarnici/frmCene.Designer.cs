namespace Testiranje.Sifarnici
{
    partial class frmCene
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCene));
            this.button2 = new System.Windows.Forms.Button();
            this.txtCena = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.cboKomitent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTipCenovnika = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrvi = new System.Windows.Forms.ToolStripButton();
            this.tsNazad = new System.Windows.Forms.ToolStripButton();
            this.tsNapred = new System.Windows.Forms.ToolStripButton();
            this.tsPoslednja = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.cboVrstaManipulacije = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtCena2 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboUvoznik = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboPostupakSaRobom = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtNazivCenovnika = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboRazred = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboOrgJed = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOznakaManipulacije = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena2)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(290, 82);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 25);
            this.button2.TabIndex = 103;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtCena
            // 
            this.txtCena.DecimalPlaces = 2;
            this.txtCena.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCena.Location = new System.Drawing.Point(565, 138);
            this.txtCena.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(105, 22);
            this.txtCena.TabIndex = 99;
            this.txtCena.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(423, 138);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(66, 15);
            this.label13.TabIndex = 101;
            this.label13.Text = "Cena JM1:";
            // 
            // cboKomitent
            // 
            this.cboKomitent.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboKomitent.FormattingEnabled = true;
            this.cboKomitent.Location = new System.Drawing.Point(135, 114);
            this.cboKomitent.Name = "cboKomitent";
            this.cboKomitent.Size = new System.Drawing.Size(210, 24);
            this.cboKomitent.TabIndex = 97;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 98;
            this.label2.Text = "Partner 1";
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSifra.Location = new System.Drawing.Point(136, 55);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(105, 22);
            this.txtSifra.TabIndex = 95;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 96;
            this.label1.Text = "Šifra:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(8, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 15);
            this.label6.TabIndex = 105;
            this.label6.Text = "Tip cenovnika:";
            // 
            // cboTipCenovnika
            // 
            this.cboTipCenovnika.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboTipCenovnika.FormattingEnabled = true;
            this.cboTipCenovnika.Location = new System.Drawing.Point(136, 83);
            this.cboTipCenovnika.Name = "cboTipCenovnika";
            this.cboTipCenovnika.Size = new System.Drawing.Size(148, 24);
            this.cboTipCenovnika.TabIndex = 104;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.DodgerBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsSave,
            this.tsDelete,
            this.toolStripSeparator1,
            this.tsPrvi,
            this.tsNazad,
            this.tsNapred,
            this.tsPoslednja,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(911, 33);
            this.toolStrip1.TabIndex = 106;
            this.toolStrip1.Text = "Štampaj izveštaj";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = ((System.Drawing.Image)(resources.GetObject("tsNew.Image")));
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(24, 30);
            this.tsNew.Text = "Novi";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(24, 30);
            this.tsSave.Text = "tsSave";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsDelete
            // 
            this.tsDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsDelete.Image")));
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(24, 30);
            this.tsDelete.Text = "toolStripButton1";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // tsPrvi
            // 
            this.tsPrvi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrvi.Image = ((System.Drawing.Image)(resources.GetObject("tsPrvi.Image")));
            this.tsPrvi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrvi.Name = "tsPrvi";
            this.tsPrvi.Size = new System.Drawing.Size(24, 30);
            this.tsPrvi.Text = "toolStripButton1";
            this.tsPrvi.Click += new System.EventHandler(this.tsPrvi_Click);
            // 
            // tsNazad
            // 
            this.tsNazad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNazad.Image = ((System.Drawing.Image)(resources.GetObject("tsNazad.Image")));
            this.tsNazad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNazad.Name = "tsNazad";
            this.tsNazad.Size = new System.Drawing.Size(24, 30);
            this.tsNazad.Text = "toolStripButton1";
            this.tsNazad.Click += new System.EventHandler(this.tsNazad_Click);
            // 
            // tsNapred
            // 
            this.tsNapred.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNapred.Image = ((System.Drawing.Image)(resources.GetObject("tsNapred.Image")));
            this.tsNapred.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNapred.Name = "tsNapred";
            this.tsNapred.Size = new System.Drawing.Size(24, 30);
            this.tsNapred.Text = "toolStripButton1";
            this.tsNapred.Click += new System.EventHandler(this.tsNapred_Click);
            // 
            // tsPoslednja
            // 
            this.tsPoslednja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPoslednja.Image = ((System.Drawing.Image)(resources.GetObject("tsPoslednja.Image")));
            this.tsPoslednja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPoslednja.Name = "tsPoslednja";
            this.tsPoslednja.Size = new System.Drawing.Size(24, 30);
            this.tsPoslednja.Text = "toolStripButton1";
            this.tsPoslednja.Click += new System.EventHandler(this.tsPoslednja_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.ForeColor = System.Drawing.Color.White;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(173, 30);
            this.toolStripButton2.Text = "Kopiranje cenovnika forma";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // cboVrstaManipulacije
            // 
            this.cboVrstaManipulacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboVrstaManipulacije.FormattingEnabled = true;
            this.cboVrstaManipulacije.Location = new System.Drawing.Point(565, 76);
            this.cboVrstaManipulacije.Name = "cboVrstaManipulacije";
            this.cboVrstaManipulacije.Size = new System.Drawing.Size(248, 24);
            this.cboVrstaManipulacije.TabIndex = 107;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(423, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 15);
            this.label3.TabIndex = 108;
            this.label3.Text = "Vrsta usluge:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 243);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(889, 440);
            this.dataGridView1.TabIndex = 109;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // txtCena2
            // 
            this.txtCena2.DecimalPlaces = 2;
            this.txtCena2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtCena2.Location = new System.Drawing.Point(565, 168);
            this.txtCena2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCena2.Name = "txtCena2";
            this.txtCena2.Size = new System.Drawing.Size(105, 22);
            this.txtCena2.TabIndex = 110;
            this.txtCena2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(423, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 15);
            this.label4.TabIndex = 111;
            this.label4.Text = "Cena JM2:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(350, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 25);
            this.button1.TabIndex = 112;
            this.button1.Text = "?";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(676, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 16);
            this.label5.TabIndex = 113;
            this.label5.Text = "EUR";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(676, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 16);
            this.label7.TabIndex = 114;
            this.label7.Text = "EUR";
            // 
            // cboUvoznik
            // 
            this.cboUvoznik.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboUvoznik.FormattingEnabled = true;
            this.cboUvoznik.Location = new System.Drawing.Point(135, 142);
            this.cboUvoznik.Name = "cboUvoznik";
            this.cboUvoznik.Size = new System.Drawing.Size(210, 24);
            this.cboUvoznik.TabIndex = 115;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(8, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 15);
            this.label8.TabIndex = 116;
            this.label8.Text = "Partner 2";
            // 
            // cboPostupakSaRobom
            // 
            this.cboPostupakSaRobom.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboPostupakSaRobom.FormattingEnabled = true;
            this.cboPostupakSaRobom.Location = new System.Drawing.Point(136, 213);
            this.cboPostupakSaRobom.Name = "cboPostupakSaRobom";
            this.cboPostupakSaRobom.Size = new System.Drawing.Size(210, 24);
            this.cboPostupakSaRobom.TabIndex = 117;
            this.cboPostupakSaRobom.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(8, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 15);
            this.label9.TabIndex = 118;
            this.label9.Text = "Postupak sa robom";
            this.label9.Visible = false;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(350, 142);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(34, 25);
            this.button3.TabIndex = 119;
            this.button3.Text = "?";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtNazivCenovnika
            // 
            this.txtNazivCenovnika.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtNazivCenovnika.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNazivCenovnika.ForeColor = System.Drawing.Color.Black;
            this.txtNazivCenovnika.Location = new System.Drawing.Point(135, 172);
            this.txtNazivCenovnika.Name = "txtNazivCenovnika";
            this.txtNazivCenovnika.Size = new System.Drawing.Size(210, 22);
            this.txtNazivCenovnika.TabIndex = 120;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(7, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 15);
            this.label10.TabIndex = 121;
            this.label10.Text = "Naziv cenovnika:";
            // 
            // cboRazred
            // 
            this.cboRazred.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboRazred.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboRazred.FormattingEnabled = true;
            this.cboRazred.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H"});
            this.cboRazred.Location = new System.Drawing.Point(565, 46);
            this.cboRazred.Name = "cboRazred";
            this.cboRazred.Size = new System.Drawing.Size(210, 24);
            this.cboRazred.TabIndex = 122;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(423, 46);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(105, 15);
            this.label11.TabIndex = 123;
            this.label11.Text = "Razred cenovnika";
            // 
            // cboOrgJed
            // 
            this.cboOrgJed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.cboOrgJed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.cboOrgJed.FormattingEnabled = true;
            this.cboOrgJed.Location = new System.Drawing.Point(565, 196);
            this.cboOrgJed.Name = "cboOrgJed";
            this.cboOrgJed.Size = new System.Drawing.Size(298, 24);
            this.cboOrgJed.TabIndex = 124;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(423, 199);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 15);
            this.label12.TabIndex = 125;
            this.label12.Text = "Organizaciona jedinica:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(423, 106);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 15);
            this.label14.TabIndex = 127;
            this.label14.Text = "Oznaka usluge:";
            // 
            // txtOznakaManipulacije
            // 
            this.txtOznakaManipulacije.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.txtOznakaManipulacije.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtOznakaManipulacije.Location = new System.Drawing.Point(565, 106);
            this.txtOznakaManipulacije.Name = "txtOznakaManipulacije";
            this.txtOznakaManipulacije.Size = new System.Drawing.Size(210, 22);
            this.txtOznakaManipulacije.TabIndex = 128;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(781, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(34, 25);
            this.button4.TabIndex = 129;
            this.button4.Text = "?";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(350, 171);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(34, 25);
            this.button5.TabIndex = 130;
            this.button5.Text = "*";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // frmCene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(911, 695);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtOznakaManipulacije);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cboOrgJed);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cboRazred);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtNazivCenovnika);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.cboPostupakSaRobom);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cboUvoznik);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtCena2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cboVrstaManipulacije);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTipCenovnika);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtCena);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cboKomitent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSifra);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCene";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cene";
            this.Load += new System.EventHandler(this.frmCene_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtCena)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCena2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown txtCena;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboKomitent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboTipCenovnika;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripButton tsSave;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsPrvi;
        private System.Windows.Forms.ToolStripButton tsNazad;
        private System.Windows.Forms.ToolStripButton tsNapred;
        private System.Windows.Forms.ToolStripButton tsPoslednja;
        private System.Windows.Forms.ComboBox cboVrstaManipulacije;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.NumericUpDown txtCena2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboUvoznik;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboPostupakSaRobom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtNazivCenovnika;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboRazred;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboOrgJed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtOznakaManipulacije;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}