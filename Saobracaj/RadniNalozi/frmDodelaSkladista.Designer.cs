
namespace Saobracaj.RadniNalozi
{
    partial class frmDodelaSkladista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDodelaSkladista));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnUnesi = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkGateInVoz = new System.Windows.Forms.CheckBox();
            this.chkGAteInKamion = new System.Windows.Forms.CheckBox();
            this.chkCIR = new System.Windows.Forms.CheckBox();
            this.chkGateINTerminal = new System.Windows.Forms.CheckBox();
            this.chkGateInKamionUvoz = new System.Windows.Forms.CheckBox();
            this.tabSplitterContainer1 = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.tabSplitterPage1 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.chkInterniPrenos = new System.Windows.Forms.CheckBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.tabSplitterPage2 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabSplitterContainer1.SuspendLayout();
            this.tabSplitterPage1.SuspendLayout();
            this.tabSplitterPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 179);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1420, 209);
            this.dataGridView1.TabIndex = 177;
            // 
            // btnUnesi
            // 
            this.btnUnesi.BackColor = System.Drawing.Color.Orange;
            this.btnUnesi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUnesi.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnUnesi.ForeColor = System.Drawing.Color.White;
            this.btnUnesi.Location = new System.Drawing.Point(371, 134);
            this.btnUnesi.Name = "btnUnesi";
            this.btnUnesi.Size = new System.Drawing.Size(255, 29);
            this.btnUnesi.TabIndex = 180;
            this.btnUnesi.Text = "Seleckovano polje >> Selectovan zapis";
            this.btnUnesi.UseVisualStyleBackColor = false;
            this.btnUnesi.Click += new System.EventHandler(this.btnUnesi_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(0, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1423, 377);
            this.dataGridView2.TabIndex = 181;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(346, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 16);
            this.label5.TabIndex = 191;
            this.label5.Text = "PrijemID - RN1";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(469, 65);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 20);
            this.textBox1.TabIndex = 194;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(139, 65);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(144, 20);
            this.textBox2.TabIndex = 195;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(23, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 16);
            this.label1.TabIndex = 196;
            this.label1.Text = "Oznaka skladišta";
            // 
            // chkGateInVoz
            // 
            this.chkGateInVoz.AutoSize = true;
            this.chkGateInVoz.BackColor = System.Drawing.Color.White;
            this.chkGateInVoz.ForeColor = System.Drawing.Color.Black;
            this.chkGateInVoz.Location = new System.Drawing.Point(26, 95);
            this.chkGateInVoz.Name = "chkGateInVoz";
            this.chkGateInVoz.Size = new System.Drawing.Size(94, 17);
            this.chkGateInVoz.TabIndex = 329;
            this.chkGateInVoz.Text = "GATE IN VOZ";
            this.chkGateInVoz.UseVisualStyleBackColor = false;
            // 
            // chkGAteInKamion
            // 
            this.chkGAteInKamion.AutoSize = true;
            this.chkGAteInKamion.BackColor = System.Drawing.Color.White;
            this.chkGAteInKamion.ForeColor = System.Drawing.Color.Black;
            this.chkGAteInKamion.Location = new System.Drawing.Point(139, 118);
            this.chkGAteInKamion.Name = "chkGAteInKamion";
            this.chkGAteInKamion.Size = new System.Drawing.Size(149, 17);
            this.chkGAteInKamion.TabIndex = 330;
            this.chkGAteInKamion.Text = "GATE IN KAMION IZVOZ";
            this.chkGAteInKamion.UseVisualStyleBackColor = false;
            // 
            // chkCIR
            // 
            this.chkCIR.AutoSize = true;
            this.chkCIR.BackColor = System.Drawing.Color.White;
            this.chkCIR.ForeColor = System.Drawing.Color.Black;
            this.chkCIR.Location = new System.Drawing.Point(26, 118);
            this.chkCIR.Name = "chkCIR";
            this.chkCIR.Size = new System.Drawing.Size(44, 17);
            this.chkCIR.TabIndex = 331;
            this.chkCIR.Text = "CIR";
            this.chkCIR.UseVisualStyleBackColor = false;
            // 
            // chkGateINTerminal
            // 
            this.chkGateINTerminal.AutoSize = true;
            this.chkGateINTerminal.BackColor = System.Drawing.Color.White;
            this.chkGateINTerminal.ForeColor = System.Drawing.Color.Black;
            this.chkGateINTerminal.Location = new System.Drawing.Point(139, 141);
            this.chkGateINTerminal.Name = "chkGateINTerminal";
            this.chkGateINTerminal.Size = new System.Drawing.Size(172, 17);
            this.chkGateINTerminal.TabIndex = 332;
            this.chkGateINTerminal.Text = "GATE IN KAMION TERMINAL";
            this.chkGateINTerminal.UseVisualStyleBackColor = false;
            // 
            // chkGateInKamionUvoz
            // 
            this.chkGateInKamionUvoz.AutoSize = true;
            this.chkGateInKamionUvoz.BackColor = System.Drawing.Color.White;
            this.chkGateInKamionUvoz.ForeColor = System.Drawing.Color.Black;
            this.chkGateInKamionUvoz.Location = new System.Drawing.Point(139, 95);
            this.chkGateInKamionUvoz.Name = "chkGateInKamionUvoz";
            this.chkGateInKamionUvoz.Size = new System.Drawing.Size(147, 17);
            this.chkGateInKamionUvoz.TabIndex = 333;
            this.chkGateInKamionUvoz.Text = "GATE IN KAMION UVOZ";
            this.chkGateInKamionUvoz.UseVisualStyleBackColor = false;
            // 
            // tabSplitterContainer1
            // 
            this.tabSplitterContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(117)))), ((int)(((byte)(123)))));
            this.tabSplitterContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSplitterContainer1.Location = new System.Drawing.Point(0, 0);
            this.tabSplitterContainer1.Name = "tabSplitterContainer1";
            this.tabSplitterContainer1.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage1});
            this.tabSplitterContainer1.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage2});
            this.tabSplitterContainer1.Size = new System.Drawing.Size(1423, 806);
            this.tabSplitterContainer1.SplitterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(117)))), ((int)(((byte)(123)))));
            this.tabSplitterContainer1.SplitterPosition = 403;
            this.tabSplitterContainer1.TabIndex = 334;
            this.tabSplitterContainer1.Text = "tabSplitterContainer1";
            // 
            // tabSplitterPage1
            // 
            this.tabSplitterPage1.AutoScroll = true;
            this.tabSplitterPage1.BackColor = System.Drawing.Color.White;
            this.tabSplitterPage1.Controls.Add(this.chkInterniPrenos);
            this.tabSplitterPage1.Controls.Add(this.txtID);
            this.tabSplitterPage1.Controls.Add(this.textBox2);
            this.tabSplitterPage1.Controls.Add(this.chkGateInKamionUvoz);
            this.tabSplitterPage1.Controls.Add(this.textBox1);
            this.tabSplitterPage1.Controls.Add(this.dataGridView1);
            this.tabSplitterPage1.Controls.Add(this.btnUnesi);
            this.tabSplitterPage1.Controls.Add(this.chkGateINTerminal);
            this.tabSplitterPage1.Controls.Add(this.label5);
            this.tabSplitterPage1.Controls.Add(this.label1);
            this.tabSplitterPage1.Controls.Add(this.chkGAteInKamion);
            this.tabSplitterPage1.Controls.Add(this.chkGateInVoz);
            this.tabSplitterPage1.Controls.Add(this.chkCIR);
            this.tabSplitterPage1.Hide = false;
            this.tabSplitterPage1.Location = new System.Drawing.Point(0, 0);
            this.tabSplitterPage1.Name = "tabSplitterPage1";
            this.tabSplitterPage1.Size = new System.Drawing.Size(1423, 403);
            this.tabSplitterPage1.TabIndex = 1;
            this.tabSplitterPage1.Text = "Polja";
            // 
            // chkInterniPrenos
            // 
            this.chkInterniPrenos.AutoSize = true;
            this.chkInterniPrenos.BackColor = System.Drawing.Color.White;
            this.chkInterniPrenos.ForeColor = System.Drawing.Color.Black;
            this.chkInterniPrenos.Location = new System.Drawing.Point(327, 95);
            this.chkInterniPrenos.Name = "chkInterniPrenos";
            this.chkInterniPrenos.Size = new System.Drawing.Size(118, 17);
            this.chkInterniPrenos.TabIndex = 388;
            this.chkInterniPrenos.Text = "INTERNI PRENOS";
            this.chkInterniPrenos.UseVisualStyleBackColor = false;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(1311, 37);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(80, 20);
            this.txtID.TabIndex = 387;
            this.txtID.Visible = false;
            // 
            // tabSplitterPage2
            // 
            this.tabSplitterPage2.AutoScroll = true;
            this.tabSplitterPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(117)))), ((int)(((byte)(123)))));
            this.tabSplitterPage2.Controls.Add(this.gridGroupingControl1);
            this.tabSplitterPage2.Controls.Add(this.dataGridView2);
            this.tabSplitterPage2.Hide = false;
            this.tabSplitterPage2.Location = new System.Drawing.Point(0, 423);
            this.tabSplitterPage2.Name = "tabSplitterPage2";
            this.tabSplitterPage2.Size = new System.Drawing.Size(1423, 383);
            this.tabSplitterPage2.TabIndex = 2;
            this.tabSplitterPage2.Text = "Kontejneri";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton7,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1423, 25);
            this.toolStrip1.TabIndex = 335;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(104, 22);
            this.toolStripButton7.Text = "MAPA SKLADISTA";
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // commandBarController1
            // 
            this.commandBarController1.HostForm = this;
            this.commandBarController1.MetroBackColor = System.Drawing.Color.White;
            this.commandBarController1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.commandBarController1.UseBackwardCompatiblity = false;
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
            this.gridGroupingControl1.Location = new System.Drawing.Point(3, 3);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2007ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2007ColorScheme.Black;
            this.gridGroupingControl1.Office2010ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2010ColorScheme.Black;
            this.gridGroupingControl1.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.Black;
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(1420, 377);
            this.gridGroupingControl1.TabIndex = 238;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.TableOptions.CaptionRowHeight = 22;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl1.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl1.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(199)))), ((int)(((byte)(249)))));
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Text = "gridGroupingControl2";
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "18.4460.0.34";
            // 
            // frmDodelaSkladista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1423, 806);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabSplitterContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDodelaSkladista";
            this.Text = "ODREDI POZICIJU";
            this.Load += new System.EventHandler(this.frmDodelaSkladista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabSplitterContainer1.ResumeLayout(false);
            this.tabSplitterPage1.ResumeLayout(false);
            this.tabSplitterPage1.PerformLayout();
            this.tabSplitterPage2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnUnesi;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkGateInVoz;
        private System.Windows.Forms.CheckBox chkGAteInKamion;
        private System.Windows.Forms.CheckBox chkCIR;
        private System.Windows.Forms.CheckBox chkGateINTerminal;
        private System.Windows.Forms.CheckBox chkGateInKamionUvoz;
        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer tabSplitterContainer1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.CheckBox chkInterniPrenos;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
    }
}