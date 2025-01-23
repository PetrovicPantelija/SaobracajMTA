namespace Saobracaj.RadniNalozi
{
    partial class frmKontejnerTekuceOperacijeLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKontejnerTekuceOperacijeLog));
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.commandBarController1 = new Syncfusion.Windows.Forms.Tools.CommandBarController(this.components);
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridGroupingControl2 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.button2.Location = new System.Drawing.Point(977, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(54, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Pokreni";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 20000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Controls.Add(this.txtSifra);
            this.panelHeader.Controls.Add(this.panel3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1323, 33);
            this.panelHeader.TabIndex = 464;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button25);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button23);
            this.panel2.Location = new System.Drawing.Point(12, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1056, 31);
            this.panel2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(238)))), ((int)(((byte)(50)))));
            this.button1.Location = new System.Drawing.Point(1087, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(63, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "2: URAĐENI";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // button25
            // 
            this.button25.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button25.Dock = System.Windows.Forms.DockStyle.Left;
            this.button25.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.button25.FlatAppearance.BorderSize = 0;
            this.button25.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.button25.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button25.Font = new System.Drawing.Font("Helvetica", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.button25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.button25.Location = new System.Drawing.Point(67, 0);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(10, 31);
            this.button25.TabIndex = 18;
            this.button25.Text = "Refresh";
            this.button25.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button25.UseVisualStyleBackColor = true;
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
            this.button23.Size = new System.Drawing.Size(67, 31);
            this.button23.TabIndex = 16;
            this.button23.Text = "Pokreni";
            this.button23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(685, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 138;
            this.label1.Text = "Šifra:";
            this.label1.Visible = false;
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtSifra.Location = new System.Drawing.Point(745, 4);
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(105, 22);
            this.txtSifra.TabIndex = 137;
            this.txtSifra.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(10, 31);
            this.panel3.TabIndex = 2;
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
            this.gridGroupingControl2.Location = new System.Drawing.Point(0, 39);
            this.gridGroupingControl2.Name = "gridGroupingControl2";
            this.gridGroupingControl2.Office2007ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2007ColorScheme.Black;
            this.gridGroupingControl2.Office2010ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2010ColorScheme.Black;
            this.gridGroupingControl2.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.Black;
            this.gridGroupingControl2.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl2.Size = new System.Drawing.Size(1311, 633);
            this.gridGroupingControl2.TabIndex = 465;
            this.gridGroupingControl2.TableDescriptor.AllowNew = false;
            this.gridGroupingControl2.TableDescriptor.TableOptions.CaptionRowHeight = 22;
            this.gridGroupingControl2.TableDescriptor.TableOptions.ColumnHeaderRowHeight = 28;
            this.gridGroupingControl2.TableDescriptor.TableOptions.RecordRowHeight = 28;
            this.gridGroupingControl2.TableOptions.AllowSelection = Syncfusion.Windows.Forms.Grid.GridSelectionFlags.None;
            this.gridGroupingControl2.TableOptions.ListBoxSelectionMode = System.Windows.Forms.SelectionMode.One;
            this.gridGroupingControl2.TableOptions.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(199)))), ((int)(((byte)(249)))));
            this.gridGroupingControl2.TableOptions.SelectionTextColor = System.Drawing.Color.White;
            this.gridGroupingControl2.Text = "gridGroupingControl2";
            this.gridGroupingControl2.UseRightToLeftCompatibleTextBox = true;
            this.gridGroupingControl2.VersionInfo = "18.4460.0.34";
            // 
            // frmKontejnerTekuceOperacijeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 684);
            this.Controls.Add(this.gridGroupingControl2);
            this.Controls.Add(this.panelHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKontejnerTekuceOperacijeLog";
            this.Text = "LOG OPERACIJA";
            this.Load += new System.EventHandler(this.frmKontejnerTekuceOperacijeLog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.commandBarController1)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private Syncfusion.Windows.Forms.Tools.CommandBarController commandBarController1;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl2;
    }
}