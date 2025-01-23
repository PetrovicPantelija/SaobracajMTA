
namespace Saobracaj.Uvoz
{
    partial class frmUvozKonacnaTable
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUvozKonacnaTable));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtNadredjeni = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.chkOpsti = new System.Windows.Forms.CheckBox();
            this.nmrOpsti = new System.Windows.Forms.NumericUpDown();
            this.dtpOpsti = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOpsti = new System.Windows.Forms.TextBox();
            this.cboOpsti = new System.Windows.Forms.ComboBox();
            this.label54 = new System.Windows.Forms.Label();
            this.cboPolje = new System.Windows.Forms.ComboBox();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrOpsti)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(64, 20);
            this.textBox1.TabIndex = 236;
            this.textBox1.Visible = false;
            // 
            // txtNadredjeni
            // 
            this.txtNadredjeni.Location = new System.Drawing.Point(12, 69);
            this.txtNadredjeni.Name = "txtNadredjeni";
            this.txtNadredjeni.Size = new System.Drawing.Size(64, 20);
            this.txtNadredjeni.TabIndex = 238;
            this.txtNadredjeni.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.chkOpsti);
            this.groupBox1.Controls.Add(this.nmrOpsti);
            this.groupBox1.Controls.Add(this.dtpOpsti);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtOpsti);
            this.groupBox1.Controls.Add(this.cboOpsti);
            this.groupBox1.Controls.Add(this.label54);
            this.groupBox1.Controls.Add(this.cboPolje);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(771, 159);
            this.groupBox1.TabIndex = 239;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Promena vrednosti na više selektovanih stavki";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button2.Location = new System.Drawing.Point(665, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 57);
            this.button2.TabIndex = 203;
            this.button2.Text = "Primeni izmenu";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkOpsti
            // 
            this.chkOpsti.AutoSize = true;
            this.chkOpsti.Checked = true;
            this.chkOpsti.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOpsti.ForeColor = System.Drawing.Color.Black;
            this.chkOpsti.Location = new System.Drawing.Point(387, 98);
            this.chkOpsti.Margin = new System.Windows.Forms.Padding(2);
            this.chkOpsti.Name = "chkOpsti";
            this.chkOpsti.Size = new System.Drawing.Size(50, 17);
            this.chkOpsti.TabIndex = 202;
            this.chkOpsti.Text = "Opšti";
            this.chkOpsti.UseVisualStyleBackColor = true;
            // 
            // nmrOpsti
            // 
            this.nmrOpsti.DecimalPlaces = 3;
            this.nmrOpsti.Location = new System.Drawing.Point(557, 97);
            this.nmrOpsti.Margin = new System.Windows.Forms.Padding(2);
            this.nmrOpsti.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nmrOpsti.Name = "nmrOpsti";
            this.nmrOpsti.Size = new System.Drawing.Size(90, 20);
            this.nmrOpsti.TabIndex = 201;
            this.nmrOpsti.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dtpOpsti
            // 
            this.dtpOpsti.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dtpOpsti.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOpsti.Location = new System.Drawing.Point(387, 135);
            this.dtpOpsti.Margin = new System.Windows.Forms.Padding(2);
            this.dtpOpsti.Name = "dtpOpsti";
            this.dtpOpsti.Size = new System.Drawing.Size(149, 20);
            this.dtpOpsti.TabIndex = 200;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(185)))), ((int)(((byte)(87)))));
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(281, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 57);
            this.button1.TabIndex = 199;
            this.button1.Text = "Izberi";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(384, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 198;
            this.label1.Text = "Nova vrednost";
            this.label1.Visible = false;
            // 
            // txtOpsti
            // 
            this.txtOpsti.Location = new System.Drawing.Point(387, 63);
            this.txtOpsti.Margin = new System.Windows.Forms.Padding(2);
            this.txtOpsti.Name = "txtOpsti";
            this.txtOpsti.Size = new System.Drawing.Size(260, 20);
            this.txtOpsti.TabIndex = 197;
            // 
            // cboOpsti
            // 
            this.cboOpsti.FormattingEnabled = true;
            this.cboOpsti.ItemHeight = 13;
            this.cboOpsti.Items.AddRange(new object[] {
            "Naziv broda",
            "Napomena 1",
            "Datum BZ",
            "PIN",
            "Vrsta kontejnera",
            "Relacija R\\L\\SRB",
            "Dirigacija kontejnera za",
            "BL",
            "ADR",
            "Brodar",
            "Vlasnik",
            "Uvoznik",
            "Nalogodavac 1",
            "Ref1",
            "Nalogodavac 2",
            "Ref2",
            "Nalogodavac3",
            "Ref3",
            "VrstaPregleda",
            "Špedicija-RTCLeget",
            "Špedicija granica",
            "Carinski postupak",
            "Postupak sa robom",
            "Način pakovanja",
            "Napomena 2",
            "Odredišna špedicija",
            "Carinarnica",
            "Mesto istovara",
            "Kontakt osoba",
            "EMail"});
            this.cboOpsti.Location = new System.Drawing.Point(387, 34);
            this.cboOpsti.Margin = new System.Windows.Forms.Padding(2);
            this.cboOpsti.Name = "cboOpsti";
            this.cboOpsti.Size = new System.Drawing.Size(260, 21);
            this.cboOpsti.TabIndex = 196;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.ForeColor = System.Drawing.Color.Black;
            this.label54.Location = new System.Drawing.Point(13, 15);
            this.label54.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(30, 13);
            this.label54.TabIndex = 195;
            this.label54.Text = "Polje";
            this.label54.Visible = false;
            // 
            // cboPolje
            // 
            this.cboPolje.FormattingEnabled = true;
            this.cboPolje.ItemHeight = 13;
            this.cboPolje.Items.AddRange(new object[] {
            "DobijenBZ",
            "Datum BZ",
            "Prioritet",
            "PIN",
            "Broj kontejnera",
            "Vrsta kontejnera",
            "Relacija R/L/SRB",
            "Dirigacija kontejnera za",
            "BL",
            "ADR",
            "Brodar ",
            "Vlasnik kontejnera",
            "Nalogodavac za voz",
            "Ref za fakturisanje 1",
            "Nalogdavac za usluge",
            "Ref za fakturisanje 2",
            "Nalogodavac za drumski prevoz",
            "Ref za fakturisanje 3",
            "Uvoznik",
            "Inspekciski tretman",
            "Špedicija-granica",
            "Špedicija-RTC Leget",
            "Carinski postupak",
            "Postupak sa robom /kontejnerom",
            "Način pakovanja",
            "Odredišna Špedicija",
            "Carinarnica",
            "Mesto istovara",
            "Adresa utovara",
            "Kontakt osobe",
            "Naslov za slanje statusa vozila",
            "E-mail za slanje statusa",
            "Napomena 2",
            "NTTO robe",
            "Tara kontejnera",
            "BTTO kontejnera",
            "BTTO robe",
            "Koleta",
            "ATA broda u Luku Rijeka",
            "Broj plombe 1",
            "Broj plombe 2"});
            this.cboPolje.Location = new System.Drawing.Point(16, 34);
            this.cboPolje.Margin = new System.Windows.Forms.Padding(2);
            this.cboPolje.Name = "cboPolje";
            this.cboPolje.Size = new System.Drawing.Size(260, 21);
            this.cboPolje.TabIndex = 57;
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.Controls.Add(this.panel4);
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1199, 33);
            this.panelHeader.TabIndex = 248;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1211, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(0, 30);
            this.panel4.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1211, 33);
            this.panel2.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.AutoSize = true;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.Dock = System.Windows.Forms.DockStyle.Left;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 33);
            this.button4.TabIndex = 20;
            this.button4.Text = "Refresh";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridGroupingControl1.ApplyVisualStyles = false;
            this.gridGroupingControl1.BackColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Custom;
            this.gridGroupingControl1.Location = new System.Drawing.Point(12, 207);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2007ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2007ColorScheme.Black;
            this.gridGroupingControl1.Office2010ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2010ColorScheme.Black;
            this.gridGroupingControl1.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.Black;
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(1187, 376);
            this.gridGroupingControl1.TabIndex = 249;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 22;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl1.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.One;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(199)))), ((int)(((byte)(249)))));
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "18.4460.0.34";
            // 
            // frmUvozKonacnaTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1199, 595);
            this.Controls.Add(this.gridGroupingControl1);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNadredjeni);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmUvozKonacnaTable";
            this.Text = "Uvoz konacna table";
            this.Load += new System.EventHandler(this.frmUvozKonacnaTable_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gridGroupingControl1_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrOpsti)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtNadredjeni;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkOpsti;
        private System.Windows.Forms.NumericUpDown nmrOpsti;
        private System.Windows.Forms.DateTimePicker dtpOpsti;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOpsti;
        private System.Windows.Forms.ComboBox cboOpsti;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.ComboBox cboPolje;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
    }
}