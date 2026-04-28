namespace Saobracaj.VSD
{
    partial class DnevniAnaliza
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
            this.tabSplitterContainer1 = new Syncfusion.Windows.Forms.Tools.TabSplitterContainer();
            this.tabSplitterPage1 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.tabSplitterPage2 = new Syncfusion.Windows.Forms.Tools.TabSplitterPage();
            this.gridGroupingControl2 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button23 = new System.Windows.Forms.Button();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.tabSplitterContainer1.SuspendLayout();
            this.tabSplitterPage1.SuspendLayout();
            this.tabSplitterPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabSplitterContainer1
            // 
            this.tabSplitterContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSplitterContainer1.Location = new System.Drawing.Point(12, 47);
            this.tabSplitterContainer1.Name = "tabSplitterContainer1";
            this.tabSplitterContainer1.PrimaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage1});
            this.tabSplitterContainer1.SecondaryPages.AddRange(new Syncfusion.Windows.Forms.Tools.TabSplitterPage[] {
            this.tabSplitterPage2});
            this.tabSplitterContainer1.Size = new System.Drawing.Size(1204, 616);
            this.tabSplitterContainer1.SplitterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.tabSplitterContainer1.SplitterPosition = 422;
            this.tabSplitterContainer1.TabIndex = 11;
            this.tabSplitterContainer1.Text = "tabSplitterContainer1";
            // 
            // tabSplitterPage1
            // 
            this.tabSplitterPage1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabSplitterPage1.AutoScroll = true;
            this.tabSplitterPage1.Controls.Add(this.gridGroupingControl1);
            this.tabSplitterPage1.Hide = false;
            this.tabSplitterPage1.Location = new System.Drawing.Point(0, 0);
            this.tabSplitterPage1.Name = "tabSplitterPage1";
            this.tabSplitterPage1.Size = new System.Drawing.Size(1204, 422);
            this.tabSplitterPage1.TabIndex = 1;
            this.tabSplitterPage1.Text = "Realizacija";
            this.tabSplitterPage1.Paint += new System.Windows.Forms.PaintEventHandler(this.tabSplitterPage1_Paint);
            // 
            // tabSplitterPage2
            // 
            this.tabSplitterPage2.AutoScroll = true;
            this.tabSplitterPage2.Controls.Add(this.gridGroupingControl2);
            this.tabSplitterPage2.Hide = false;
            this.tabSplitterPage2.Location = new System.Drawing.Point(0, 442);
            this.tabSplitterPage2.Name = "tabSplitterPage2";
            this.tabSplitterPage2.Size = new System.Drawing.Size(1204, 174);
            this.tabSplitterPage2.TabIndex = 2;
            this.tabSplitterPage2.Text = "KONTEJNERI";
            this.tabSplitterPage2.ToolTip = "Komercijalista";
            // 
            // gridGroupingControl2
            // 
            this.gridGroupingControl2.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridGroupingControl2.ApplyVisualStyles = false;
            this.gridGroupingControl2.BackColor = System.Drawing.Color.White;
            this.gridGroupingControl2.Font = new System.Drawing.Font("Helvetica", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl2.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl2.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl2.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Custom;
            this.gridGroupingControl2.Location = new System.Drawing.Point(0, 5);
            this.gridGroupingControl2.Name = "gridGroupingControl2";
            this.gridGroupingControl2.Office2007ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2007ColorScheme.Black;
            this.gridGroupingControl2.Office2010ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2010ColorScheme.Black;
            this.gridGroupingControl2.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.Black;
            this.gridGroupingControl2.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl2.Size = new System.Drawing.Size(1201, 149);
            this.gridGroupingControl2.TabIndex = 237;
            this.gridGroupingControl2.TableDescriptor.AllowNew = false;
            this.gridGroupingControl2.TableDescriptor.TableOptions.CaptionRowHeight = 22;
            this.gridGroupingControl2.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl2.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl2.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.gridGroupingControl2.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(199)))), ((int)(((byte)(249)))));
            this.gridGroupingControl2.TableOptions.SelectionTextColor = System.Drawing.Color.White;
            this.gridGroupingControl2.Text = "gridGroupingControl2";
            this.gridGroupingControl2.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl2.VersionInfo = "18.4460.0.34";
            this.gridGroupingControl2.TableControlCellClick += new Syncfusion.Windows.Forms.Grid.Grouping.GridTableControlCellClickEventHandler(this.gridGroupingControl2_TableControlCellClick);
            this.gridGroupingControl2.Click += new System.EventHandler(this.gridGroupingControl2_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.button23);
            this.panel2.Location = new System.Drawing.Point(12, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1204, 41);
            this.panel2.TabIndex = 12;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1071, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.Visible = false;
            // 
            // button23
            // 
            this.button23.AutoSize = true;
            this.button23.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button23.Dock = System.Windows.Forms.DockStyle.Left;
            this.button23.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button23.FlatAppearance.BorderSize = 0;
            this.button23.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button23.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button23.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button23.Location = new System.Drawing.Point(0, 0);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(74, 41);
            this.button23.TabIndex = 16;
            this.button23.Text = "Refresh";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // gridGroupingControl1
            // 
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gridGroupingControl1.BackColor = System.Drawing.SystemColors.Window;
            this.gridGroupingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Office2016White;
            this.gridGroupingControl1.Location = new System.Drawing.Point(0, 0);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.White;
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(1204, 422);
            this.gridGroupingControl1.TabIndex = 2;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl1.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl1.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gridGroupingControl1.TableOptions.SelectionTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.gridGroupingControl1.Text = "gridGroupingControl1";
            this.gridGroupingControl1.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl1.VersionInfo = "18.4460.0.34";
            // 
            // DnevniAnaliza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 695);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tabSplitterContainer1);
            this.Name = "DnevniAnaliza";
            this.Text = "DnevniAnaliza";
            this.tabSplitterContainer1.ResumeLayout(false);
            this.tabSplitterPage1.ResumeLayout(false);
            this.tabSplitterPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TabSplitterContainer tabSplitterContainer1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage1;
        private Syncfusion.Windows.Forms.Tools.TabSplitterPage tabSplitterPage2;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.TextBox textBox1;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
    }
}