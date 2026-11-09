namespace Saobracaj.RNI
{
    partial class frmTerminalPrivremeniArhiva
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPregledLog = new System.Windows.Forms.Button();
            this.btnZakaciDokumenta = new System.Windows.Forms.Button();
            this.btnZakaciSlike = new System.Windows.Forms.Button();
            this.gridGroupingControl1 = new Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl();
            this.panelHeader.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).BeginInit();
            this.SuspendLayout();
            //
            // panelHeader
            //
            this.panelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.panel2);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1390, 33);
            this.panelHeader.TabIndex = 461;
            //
            // panel2
            //
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.btnPregledLog);
            this.panel2.Controls.Add(this.btnZakaciDokumenta);
            this.panel2.Controls.Add(this.btnZakaciSlike);
            this.panel2.Location = new System.Drawing.Point(3, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1380, 31);
            this.panel2.TabIndex = 6;
            //
            // btnPregledLog
            //
            this.btnPregledLog.AutoSize = true;
            this.btnPregledLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPregledLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPregledLog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnPregledLog.FlatAppearance.BorderSize = 0;
            this.btnPregledLog.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnPregledLog.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnPregledLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPregledLog.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnPregledLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnPregledLog.Location = new System.Drawing.Point(270, 0);
            this.btnPregledLog.Name = "btnPregledLog";
            this.btnPregledLog.Size = new System.Drawing.Size(120, 31);
            this.btnPregledLog.TabIndex = 3;
            this.btnPregledLog.Text = "Pregled log";
            this.btnPregledLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPregledLog.UseVisualStyleBackColor = true;
            this.btnPregledLog.Click += new System.EventHandler(this.btnPregledLog_Click);
            //
            // btnZakaciDokumenta
            //
            this.btnZakaciDokumenta.AutoSize = true;
            this.btnZakaciDokumenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZakaciDokumenta.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnZakaciDokumenta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnZakaciDokumenta.FlatAppearance.BorderSize = 0;
            this.btnZakaciDokumenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnZakaciDokumenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnZakaciDokumenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZakaciDokumenta.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnZakaciDokumenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnZakaciDokumenta.Location = new System.Drawing.Point(120, 0);
            this.btnZakaciDokumenta.Name = "btnZakaciDokumenta";
            this.btnZakaciDokumenta.Size = new System.Drawing.Size(150, 31);
            this.btnZakaciDokumenta.TabIndex = 2;
            this.btnZakaciDokumenta.Text = "Zakači dokumenta";
            this.btnZakaciDokumenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZakaciDokumenta.UseVisualStyleBackColor = true;
            this.btnZakaciDokumenta.Click += new System.EventHandler(this.btnZakaciDokumenta_Click);
            //
            // btnZakaciSlike
            //
            this.btnZakaciSlike.AutoSize = true;
            this.btnZakaciSlike.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZakaciSlike.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnZakaciSlike.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.btnZakaciSlike.FlatAppearance.BorderSize = 0;
            this.btnZakaciSlike.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnZakaciSlike.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnZakaciSlike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZakaciSlike.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnZakaciSlike.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.btnZakaciSlike.Location = new System.Drawing.Point(0, 0);
            this.btnZakaciSlike.Name = "btnZakaciSlike";
            this.btnZakaciSlike.Size = new System.Drawing.Size(120, 31);
            this.btnZakaciSlike.TabIndex = 1;
            this.btnZakaciSlike.Text = "Zakači slike";
            this.btnZakaciSlike.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnZakaciSlike.UseVisualStyleBackColor = true;
            this.btnZakaciSlike.Click += new System.EventHandler(this.btnZakaciSlike_Click);
            //
            // gridGroupingControl1
            //
            this.gridGroupingControl1.AlphaBlendSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.ApplyVisualStyles = false;
            this.gridGroupingControl1.BackColor = System.Drawing.Color.White;
            this.gridGroupingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGroupingControl1.Font = new System.Drawing.Font("Arial", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridGroupingControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.GridLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(54)))));
            this.gridGroupingControl1.GridOfficeScrollBars = Syncfusion.Windows.Forms.OfficeScrollBars.Office2016;
            this.gridGroupingControl1.GridVisualStyles = Syncfusion.Windows.Forms.GridVisualStyles.Custom;
            this.gridGroupingControl1.Location = new System.Drawing.Point(0, 33);
            this.gridGroupingControl1.Name = "gridGroupingControl1";
            this.gridGroupingControl1.Office2007ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2007ColorScheme.Black;
            this.gridGroupingControl1.Office2010ScrollBarsColorScheme = Syncfusion.Windows.Forms.Office2010ColorScheme.Black;
            this.gridGroupingControl1.Office2016ScrollBarsColorScheme = Syncfusion.Windows.Forms.ScrollBarOffice2016ColorScheme.Black;
            this.gridGroupingControl1.ShowCurrentCellBorderBehavior = Syncfusion.Windows.Forms.Grid.GridShowCurrentCellBorder.GrayWhenLostFocus;
            this.gridGroupingControl1.Size = new System.Drawing.Size(1390, 741);
            this.gridGroupingControl1.TabIndex = 236;
            this.gridGroupingControl1.TableDescriptor.AllowEdit = false;
            this.gridGroupingControl1.TableDescriptor.AllowNew = false;
            this.gridGroupingControl1.TableDescriptor.AllowRemove = false;
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
            // frmTerminalPrivremeniArhiva
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.ClientSize = new System.Drawing.Size(1390, 774);
            this.Controls.Add(this.gridGroupingControl1);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmTerminalPrivremeniArhiva";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Arhivirani podaci";
            this.Load += new System.EventHandler(this.frmTerminalPrivremeniArhiva_Load);
            this.panelHeader.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGroupingControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPregledLog;
        private System.Windows.Forms.Button btnZakaciDokumenta;
        private System.Windows.Forms.Button btnZakaciSlike;
        private Syncfusion.Windows.Forms.Grid.Grouping.GridGroupingControl gridGroupingControl1;
    }
}
