namespace Saobracaj.Uvoz
{
    partial class frmDodatneUsluge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDodatneUsluge));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtKonkretnaMan = new System.Windows.Forms.TextBox();
            this.txtNalogID = new System.Windows.Forms.TextBox();
            this.txtBrojOsnov = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkPotvrdiUradjen = new System.Windows.Forms.CheckBox();
            this.lblKolicina2 = new System.Windows.Forms.Label();
            this.num2 = new System.Windows.Forms.NumericUpDown();
            this.lblKolicina1 = new System.Windows.Forms.Label();
            this.num1 = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.txtTara = new System.Windows.Forms.NumericUpDown();
            this.label31 = new System.Windows.Forms.Label();
            this.txtNapomena = new System.Windows.Forms.TextBox();
            this.cboUsluga = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboBukingPrijema = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.chkUvoz = new System.Windows.Forms.CheckBox();
            this.chkIzvoz = new System.Windows.Forms.CheckBox();
            this.chkDodatne = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTara)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.splitContainer1.Panel1.Controls.Add(this.txtKonkretnaMan);
            this.splitContainer1.Panel1.Controls.Add(this.txtNalogID);
            this.splitContainer1.Panel1.Controls.Add(this.txtBrojOsnov);
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.chkUvoz);
            this.splitContainer1.Panel1.Controls.Add(this.chkIzvoz);
            this.splitContainer1.Panel1.Controls.Add(this.chkDodatne);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(1314, 661);
            this.splitContainer1.SplitterDistance = 423;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtKonkretnaMan
            // 
            this.txtKonkretnaMan.Enabled = false;
            this.txtKonkretnaMan.Location = new System.Drawing.Point(343, 57);
            this.txtKonkretnaMan.Name = "txtKonkretnaMan";
            this.txtKonkretnaMan.Size = new System.Drawing.Size(65, 20);
            this.txtKonkretnaMan.TabIndex = 256;
            // 
            // txtNalogID
            // 
            this.txtNalogID.Enabled = false;
            this.txtNalogID.Location = new System.Drawing.Point(343, 83);
            this.txtNalogID.Name = "txtNalogID";
            this.txtNalogID.Size = new System.Drawing.Size(65, 20);
            this.txtNalogID.TabIndex = 255;
            // 
            // txtBrojOsnov
            // 
            this.txtBrojOsnov.Enabled = false;
            this.txtBrojOsnov.Location = new System.Drawing.Point(343, 31);
            this.txtBrojOsnov.Name = "txtBrojOsnov";
            this.txtBrojOsnov.Size = new System.Drawing.Size(65, 20);
            this.txtBrojOsnov.TabIndex = 254;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(24, 130);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 251;
            this.button3.Text = "Pozovi";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.chkPotvrdiUradjen);
            this.panel1.Controls.Add(this.lblKolicina2);
            this.panel1.Controls.Add(this.num2);
            this.panel1.Controls.Add(this.lblKolicina1);
            this.panel1.Controls.Add(this.num1);
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.txtTara);
            this.panel1.Controls.Add(this.label31);
            this.panel1.Controls.Add(this.txtNapomena);
            this.panel1.Controls.Add(this.cboUsluga);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboBukingPrijema);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Location = new System.Drawing.Point(12, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(396, 465);
            this.panel1.TabIndex = 2;
            // 
            // chkPotvrdiUradjen
            // 
            this.chkPotvrdiUradjen.AutoSize = true;
            this.chkPotvrdiUradjen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.chkPotvrdiUradjen.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chkPotvrdiUradjen.ForeColor = System.Drawing.Color.Black;
            this.chkPotvrdiUradjen.Location = new System.Drawing.Point(29, 218);
            this.chkPotvrdiUradjen.Name = "chkPotvrdiUradjen";
            this.chkPotvrdiUradjen.Size = new System.Drawing.Size(114, 20);
            this.chkPotvrdiUradjen.TabIndex = 255;
            this.chkPotvrdiUradjen.Text = "Potvrdi Uradjen";
            this.chkPotvrdiUradjen.UseVisualStyleBackColor = false;
            // 
            // lblKolicina2
            // 
            this.lblKolicina2.AutoSize = true;
            this.lblKolicina2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.lblKolicina2.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.lblKolicina2.ForeColor = System.Drawing.Color.White;
            this.lblKolicina2.Location = new System.Drawing.Point(155, 155);
            this.lblKolicina2.Name = "lblKolicina2";
            this.lblKolicina2.Size = new System.Drawing.Size(64, 14);
            this.lblKolicina2.TabIndex = 254;
            this.lblKolicina2.Text = "KOLICINA:";
            // 
            // num2
            // 
            this.num2.DecimalPlaces = 3;
            this.num2.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.num2.Location = new System.Drawing.Point(156, 181);
            this.num2.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.num2.Name = "num2";
            this.num2.Size = new System.Drawing.Size(93, 20);
            this.num2.TabIndex = 253;
            this.num2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKolicina1
            // 
            this.lblKolicina1.AutoSize = true;
            this.lblKolicina1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.lblKolicina1.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.lblKolicina1.ForeColor = System.Drawing.Color.White;
            this.lblKolicina1.Location = new System.Drawing.Point(25, 155);
            this.lblKolicina1.Name = "lblKolicina1";
            this.lblKolicina1.Size = new System.Drawing.Size(64, 14);
            this.lblKolicina1.TabIndex = 250;
            this.lblKolicina1.Text = "KOLICINA:";
            // 
            // num1
            // 
            this.num1.DecimalPlaces = 3;
            this.num1.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.num1.Location = new System.Drawing.Point(26, 181);
            this.num1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.num1.Name = "num1";
            this.num1.Size = new System.Drawing.Size(93, 20);
            this.num1.TabIndex = 249;
            this.num1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label25.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(-605, 264);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(73, 14);
            this.label25.TabIndex = 246;
            this.label25.Text = "TARA KONT:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(22, 381);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 252;
            this.button4.Text = "Sačuvaj promenu";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // txtTara
            // 
            this.txtTara.DecimalPlaces = 3;
            this.txtTara.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.txtTara.Location = new System.Drawing.Point(-604, 279);
            this.txtTara.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtTara.Name = "txtTara";
            this.txtTara.Size = new System.Drawing.Size(93, 20);
            this.txtTara.TabIndex = 245;
            this.txtTara.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(180)))), ((int)(((byte)(137)))));
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(26, 257);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(71, 13);
            this.label31.TabIndex = 248;
            this.label31.Text = "NAPOMENE ";
            // 
            // txtNapomena
            // 
            this.txtNapomena.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtNapomena.Location = new System.Drawing.Point(22, 284);
            this.txtNapomena.Multiline = true;
            this.txtNapomena.Name = "txtNapomena";
            this.txtNapomena.Size = new System.Drawing.Size(299, 78);
            this.txtNapomena.TabIndex = 247;
            // 
            // cboUsluga
            // 
            this.cboUsluga.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.cboUsluga.FormattingEnabled = true;
            this.cboUsluga.Location = new System.Drawing.Point(22, 112);
            this.cboUsluga.Name = "cboUsluga";
            this.cboUsluga.Size = new System.Drawing.Size(264, 22);
            this.cboUsluga.TabIndex = 233;
            this.cboUsluga.SelectedIndexChanged += new System.EventHandler(this.cboUsluga_SelectedIndexChanged);
            this.cboUsluga.SelectedValueChanged += new System.EventHandler(this.cboUsluga_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.label2.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(19, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 14);
            this.label2.TabIndex = 234;
            this.label2.Text = "DODATNA USLUGA:";
            // 
            // cboBukingPrijema
            // 
            this.cboBukingPrijema.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.cboBukingPrijema.FormattingEnabled = true;
            this.cboBukingPrijema.Location = new System.Drawing.Point(22, 45);
            this.cboBukingPrijema.Name = "cboBukingPrijema";
            this.cboBukingPrijema.Size = new System.Drawing.Size(264, 22);
            this.cboBukingPrijema.TabIndex = 231;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.label15.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(19, 19);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 14);
            this.label15.TabIndex = 232;
            this.label15.Text = "DOLAZNI VOZ";
            // 
            // chkUvoz
            // 
            this.chkUvoz.AutoSize = true;
            this.chkUvoz.Location = new System.Drawing.Point(24, 63);
            this.chkUvoz.Name = "chkUvoz";
            this.chkUvoz.Size = new System.Drawing.Size(168, 17);
            this.chkUvoz.TabIndex = 1;
            this.chkUvoz.Text = "Usluge predefinisane od Uvoz";
            this.chkUvoz.UseVisualStyleBackColor = true;
            // 
            // chkIzvoz
            // 
            this.chkIzvoz.AutoSize = true;
            this.chkIzvoz.Location = new System.Drawing.Point(24, 31);
            this.chkIzvoz.Name = "chkIzvoz";
            this.chkIzvoz.Size = new System.Drawing.Size(174, 17);
            this.chkIzvoz.TabIndex = 0;
            this.chkIzvoz.Text = "Usluge predefinisane od Izvoza";
            this.chkIzvoz.UseVisualStyleBackColor = true;
            // 
            // chkDodatne
            // 
            this.chkDodatne.AutoSize = true;
            this.chkDodatne.Location = new System.Drawing.Point(24, 98);
            this.chkDodatne.Name = "chkDodatne";
            this.chkDodatne.Size = new System.Drawing.Size(171, 17);
            this.chkDodatne.TabIndex = 3;
            this.chkDodatne.Text = "Unos dedatnih usluga za Uvoz";
            this.chkDodatne.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.gridGroupingControl1);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(857, 639);
            this.panel3.TabIndex = 235;
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gridGroupingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridGroupingControl1.BackColor = System.Drawing.SystemColors.Window;
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2010Blue;
            this.gridGroupingControl1.Location = new System.Drawing.Point(3, 3);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.White;
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(851, 633);
            this.gridGroupingControl1.TabIndex = 10;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 22;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl1.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.SystemColors.Desktop;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "18.4460.0.34";
            this.gridGroupingControl1.TableControlCellClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(this.gridGroupingControl1_TableControlCellClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(122, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 256;
            this.button1.Text = "Dodeli slike";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(292, 112);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 23);
            this.button2.TabIndex = 257;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmDodatneUsluge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 661);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDodatneUsluge";
            this.Text = "UNOS DODATNIH USLUGA";
            this.Load += new System.EventHandler(this.frmDodatneUsluge_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTara)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkUvoz;
        private System.Windows.Forms.CheckBox chkIzvoz;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboBukingPrijema;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox chkDodatne;
        private System.Windows.Forms.ComboBox cboUsluga;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.NumericUpDown txtTara;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox txtNapomena;
        private System.Windows.Forms.Label lblKolicina1;
        private System.Windows.Forms.NumericUpDown num1;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox txtBrojOsnov;
        private System.Windows.Forms.TextBox txtKonkretnaMan;
        private System.Windows.Forms.TextBox txtNalogID;
        private System.Windows.Forms.Label lblKolicina2;
        private System.Windows.Forms.NumericUpDown num2;
        private System.Windows.Forms.CheckBox chkPotvrdiUradjen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}